using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core.sh2
{
    public class Sh2Interpreter
    {
        private CPU _cpu;
        private Bus _bus;

        // Битовые флаги из регистра статуса (SR)
        private bool GetT() => (_cpu.SR & 0x1) != 0;
        private bool GetS() => (_cpu.SR >> 1 & 0x1) != 0;
        private bool GetQ() => (_cpu.SR >> 8 & 0x1) != 0;
        private bool GetM() => (_cpu.SR >> 9 & 0x1) != 0;

        private void SetT(bool value)
        {
            _cpu.SR &= ~0x1U;
            _cpu.SR |= value ? 1U : 0U;
        }

        private void SetQ(bool value)
        {
            _cpu.SR &= ~0x100U;
            _cpu.SR |= (value ? 1U : 0U) << 8;
        }

        private void SetM(bool value)
        {
            _cpu.SR &= ~0x200U;
            _cpu.SR |= (value ? 1U : 0U) << 9;
        }

        public void Interpreter(CPU cpu, Bus bus)
        {
            _cpu = cpu;
            _bus = bus;
        }

        private void HandleJump(uint dst, bool delaySlot)
        {
            // TODO: выбросить исключение если эта функция вызвана в слоте задержки

            // Небольшой хак: если в слоте задержки, немедленно выполнить следующую инструкцию
            if (delaySlot)
            {
                _cpu.PC += 2;
                ushort instr = _bus.Read16(_cpu.PC - 4);
                Run(instr);
            }

            _cpu.PC = dst + 2;
        }

        private uint GetControlReg(int index)
        {
            return index switch
            {
                0 => _cpu.SR,
                1 => _cpu.GBR,
                2 => _cpu.VBR,
                _ => throw new ArgumentException("Invalid control register index")
            };
        }

        private void SetControlReg(int index, uint value)
        {
            switch (index)
            {
                case 0:
                    //SetSR(value);
                    break;
                case 1:
                    _cpu.GBR = value;
                    break;
                case 2:
                    _cpu.VBR = value;
                    break;
                default:
                    throw new ArgumentException("Invalid control register index");
            }
        }

        private uint GetSystemReg(int index)
        {
            return index switch
            {
                0 => _cpu.MACH,
                1 => _cpu.MACL,
                2 => _cpu.PR,
                _ => throw new ArgumentException("Invalid system register index")
            };
        }

        private void SetSystemReg(int index, uint value)
        {
            switch (index)
            {
                case 0:
                    _cpu.MACH = value;
                    break;
                case 1:
                    _cpu.MACL = value;
                    break;
                case 2:
                    _cpu.PR = value;
                    break;
                default:
                    throw new ArgumentException("Invalid system register index");
            }
        }

        // Инструкции передачи данных
        private void MovImm(ushort instr)
        {
            int imm = (sbyte)(instr & 0xFF);
            int reg = instr >> 8 & 0xF;
            _cpu.GPR[reg] = (uint)imm;
        }

        private void MovwPcrelReg(ushort instr)
        {
            int offs = (instr & 0xFF) << 1;
            int reg = instr >> 8 & 0xF;
            _cpu.GPR[reg] = (uint)(short)_bus.Read16((uint)(_cpu.PC + offs));
        }

        //TODO: ... и так далее для всех инструкций

        public void Run(ushort instr)
        {
            switch (instr & 0xF000)
            {
                case 0xE000:
                    MovImm(instr);
                    break;
                case 0x9000:
                    MovwPcrelReg(instr);
                    break;
                //TODO: ... и так далее для всех инструкций
                default:
                    throw new ArgumentException($"Unrecognized instruction {instr:X4} at {_cpu.PC - 4:X8}");
            }
        }

        // Арифметические инструкции
        private void Add(ushort instr)
        {
            int src = instr >> 4 & 0xF;
            int dst = instr >> 8 & 0xF;
            _cpu.GPR[dst] += _cpu.GPR[src];
        }

        private void AddC(ushort instr)
        {
            int src = instr >> 4 & 0xF;
            int dst = instr >> 8 & 0xF;

            bool oldCarry = GetT();
            uint tmp = _cpu.GPR[dst] + _cpu.GPR[src];
            uint oldDst = _cpu.GPR[dst];
            _cpu.GPR[dst] = tmp + (oldCarry ? 1U : 0U);

            bool newCarry = tmp < _cpu.GPR[dst] || oldDst > tmp;
            SetT(newCarry);
        }

        // Логические инструкции
        private void And(ushort instr)
        {
            int src = instr >> 4 & 0xF;
            int dst = instr >> 8 & 0xF;
            _cpu.GPR[dst] &= _cpu.GPR[src];
        }

        private void Or(ushort instr)
        {
            int src = instr >> 4 & 0xF;
            int dst = instr >> 8 & 0xF;
            _cpu.GPR[dst] |= _cpu.GPR[src];
        }

        // Инструкции сдвига
        private void Shal(ushort instr)
        {
            int reg = instr >> 8 & 0xF;
            SetT((_cpu.GPR[reg] & 0x80000000) != 0);
            _cpu.GPR[reg] <<= 1;
        }

        private void Shar(ushort instr)
        {
            int reg = instr >> 8 & 0xF;
            SetT((_cpu.GPR[reg] & 1) != 0);
            _cpu.GPR[reg] = (uint)((int)_cpu.GPR[reg] >> 1);
        }

        // Инструкции управления
        private void Bf(ushort instr)
        {
            int offs = (sbyte)(instr & 0xFF);
            offs <<= 1;

            uint dst = _cpu.PC + (uint)offs;
            if (!GetT())
            {
                HandleJump(dst, false);
            }
        }

        private void Bra(ushort instr)
        {
            int offs = instr & 0x7FF | ((instr & 0x800) != 0 ? unchecked((int)0xFFFFF800) : 0);
            offs <<= 1;

            uint dst = _cpu.PC + (uint)offs;
            HandleJump(dst, true);
        }
    }
}

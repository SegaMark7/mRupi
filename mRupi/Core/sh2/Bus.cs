using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core.sh2
{
    public class Bus
    {
        private static readonly CPU cpu;
        private static uint TranslateAddress(uint addr)
        {
            // Биты 28-31 игнорируются
            // Встроенный регион (биты 24-27 == 0xF) не зеркалируется, все остальные регионы зеркалируются
            if ((addr & 0x0F000000) != 0x0F000000)
            {
                return addr & ~0xF8000000;
            }
            return addr & ~0xF0000000;
        }

        private static byte UnmappedRead8(uint addr)
        {
            Console.WriteLine($"[SH2] unmapped read8 {addr:X8}");
            return 0;
        }

        private static ushort UnmappedRead16(uint addr)
        {
            Console.WriteLine($"[SH2] unmapped read16 {addr:X8}");
            return 0;
        }

        private static uint UnmappedRead32(uint addr)
        {
            Console.WriteLine($"[SH2] unmapped read32 {addr:X8}");
            return 0;
        }

        private static void UnmappedWrite8(uint addr, byte value)
        {
            Console.WriteLine($"[SH2] unmapped write8 {addr:X8}: {value:X2}");
        }

        private static void UnmappedWrite16(uint addr, ushort value)
        {
            Console.WriteLine($"[SH2] unmapped write16 {addr:X8}: {value:X4}");
        }

        private static void UnmappedWrite32(uint addr, uint value)
        {
            Console.WriteLine($"[SH2] unmapped write32 {addr:X8}: {value:X8}");
        }

        public static byte Read8(uint addr)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                return mem[addr & 0xFFF];
            }
            return UnmappedRead8(addr);
        }

        public ushort Read16(uint addr)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                return BitConverter.ToUInt16(mem, (int)(addr & 0xFFF));
            }
            return UnmappedRead16(addr);
        }

        public static uint Read32(uint addr)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                return BitConverter.ToUInt32(mem, (int)(addr & 0xFFF));
            }
            return UnmappedRead32(addr);
        }

        public static void Write8(uint addr, byte value)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                mem[addr & 0xFFF] = value;
                return;
            }
            UnmappedWrite8(addr, value);
        }

        public static void Write16(uint addr, ushort value)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                BitConverter.GetBytes(value).CopyTo(mem, (int)(addr & 0xFFF));
                return;
            }
            UnmappedWrite16(addr, value);
        }

        public static void Write32(uint addr, uint value)
        {
            addr = TranslateAddress(addr);
            byte[] mem = cpu.PageTable[addr >> 12];
            if (mem != null)
            {
                BitConverter.GetBytes(value).CopyTo(mem, (int)(addr & 0xFFF));
                return;
            }
            UnmappedWrite32(addr, value);
        }
    }
}

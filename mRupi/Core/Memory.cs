using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core
{
    class Memory
    {
        //SH2 ignores bits 28-31, so the pagetable size can be reduced
        //TODO: instead of reducing size, maybe make the pagetable more granular?
         static readonly int SH2_PAGETABLE_SIZE = (1 << 28) / 4096;
         static readonly int SH2_REGION_SIZE = 1 << 24;

         static readonly uint BIOS_START = 0x00000000;
         static readonly uint BIOS_SIZE = 0x8000;
         static readonly uint RAM_START = 0x01000000;
         static readonly uint RAM_SIZE = 0x80000;
         static readonly uint MMIO_START = 0x05000000;

        private struct State
        {
            public List<byte[]> Sh2Pagetable;
            public byte[] Bios;
            public byte[] Ram;

            public State(int biosSize, int ramSize)
            {
                Sh2Pagetable = new List<byte[]>(new byte[SH2_PAGETABLE_SIZE][]);
                Bios = new byte[biosSize];
                Ram = new byte[ramSize];
            }
        }

        private static State? state;

        private static void MapPagetable(List<byte[]> table, byte[] data, uint start, uint size)
        {
            start >>= 12;
            size >>= 12;

            for (uint i = 0; i < size; i++)
            {
                table[(int)(start + i)] = data[(int)(i << 12)..];
            }
        }

        public static void Initialize(byte[] biosRom)
        {
            state = new State((int)BIOS_START, (int)RAM_SIZE);

            Array.Copy(biosRom.ToArray(), state.Value.Bios, BIOS_SIZE);

            for (int i = 0; i < SH2_PAGETABLE_SIZE; i++)
            {
                state.Value.Sh2Pagetable[i] = null;
            }

            MapSh2Pagetable(state.Value.Bios, BIOS_START, BIOS_SIZE);

            // Отражение RAM на весь регион
            for (uint i = 0; i < SH2_REGION_SIZE; i += RAM_SIZE)
            {
                MapSh2Pagetable(state.Value.Ram, RAM_START + i, RAM_SIZE);
            }

            // VRAM маппится видеоподсистемой
        }

        public static void Shutdown()
        {
            state = null;
        }

        public static void MapSh2Pagetable(byte[] data, uint start, uint size)
        {
            if (state.HasValue)
            {
                MapPagetable(state.Value.Sh2Pagetable, data, start, size);
            }
        }

        public static byte[][]? GetSh2Pagetable()
        {
            return state?.Sh2Pagetable.ToArray();
        }
    }
}

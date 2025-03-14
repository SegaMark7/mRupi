using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core
{
    public static class Cart
    {
        static readonly uint SRAM_START = 0x02000000;
        static readonly uint ROM_START = 0x06000000;

        private class State
        {
            public List<byte> Rom { get; set; } = new();
            public List<byte> Sram { get; set; } = new();
            public string SramFilePath { get; set; } = string.Empty;
        }

        private static readonly State state = new();
        private static int frameCount = 0;

        private static void CommitSram()
        {
            File.WriteAllBytes(state.SramFilePath, state.Sram.ToArray());
        }

        public static void Initialize(byte[] info)
        {
            //state.Rom = new List<byte>(info.Rom);
            //state.Sram = new List<byte>(info.Sram);
            //state.SramFilePath = info.SramFilePath;

            // Убедимся, что ROM и SRAM выровнены по границе 4 КБ
            if (state.Rom.Count % 0x1000 != 0)
            {
                int newSize = (state.Rom.Count + 0xFFF) & ~0xFFF;
                state.Rom.AddRange(new byte[newSize - state.Rom.Count]);
                for (int i = state.Rom.Count; i < newSize; i++)
                {
                    state.Rom[i] = 0xFF;
                }
            }

            if (state.Sram.Count % 0x1000 != 0)
            {
                int newSize = (state.Sram.Count + 0xFFF) & ~0xFFF;
                state.Sram.AddRange(new byte[newSize - state.Sram.Count]);
                for (int i = state.Sram.Count; i < newSize; i++)
                {
                    state.Sram[i] = 0xFF;
                }
            }

            Memory.MapSh2Pagetable([.. state.Rom], ROM_START, (uint)state.Rom.Count);
            Memory.MapSh2Pagetable([.. state.Sram], SRAM_START, (uint)state.Sram.Count);
        }

        public static void Shutdown()
        {
            CommitSram();
        }

        public static void SramCommitCheck()
        {
            // Принудительное сохранение каждые 60 кадров
            frameCount++;

            if (frameCount < 60)
            {
                return;
            }

            frameCount = 0;
            CommitSram();
        }
    }
}

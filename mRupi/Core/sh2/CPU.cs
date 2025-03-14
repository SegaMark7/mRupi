using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core.sh2
{
    public class CPU
    {
        // Регистры общего назначения
        public uint[] GPR { get; private set; } = new uint[16];
        public uint PC { get; set; }  // Program Counter
        public uint PR { get; set; }  // Procedure Register
        public uint MACL { get; set; } // Multiply-and-Accumulate Low
        public uint MACH { get; set; } // Multiply-and-Accumulate High
        public uint GBR { get; set; }  // Global Base Register
        public uint VBR { get; set; }  // Vector Base Register
        public uint SR { get; set; }   // Status Register

        // Состояние процессора
        public int CyclesLeft { get; set; }
        public int PendingIrqPrio { get; set; }
        public int PendingIrqVector { get; set; }

        // Таблица страниц памяти
        public byte[][] PageTable { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.Core
{
    class Emu
    {
        public static void Initialize(Config.SystemInfo config)
        {
            //Memory must initialize first
            //Memory.Initialize(config.BiosRom);

            ////Ensure that timing initializes before any CPUs
            //Timing::initialize();

            ////Initialize CPUs
            //SH2::initialize();

            ////Initialize core hardware
            Cart.Initialize(config.CartRom);
            //LoopyIO::initialize();

            ////Initialize subprojects after everything else
            //Input::initialize();
            //Video::initialize();
            //Sound::initialize(config.sound_rom);

            ////Hook up connections between modules
            //SH2::OCPM::Serial::set_tx_callback(1, &Sound::midi_byte_in);
        }
    }
}

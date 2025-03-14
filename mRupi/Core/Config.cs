namespace mRupi.Core;

public class Config
{
    public class SystemInfo
    {
        public byte[] CartRom { get; set; }
        public byte[] BiosRom { get; set; }
        public byte[] SoundRom { get; set; }
        public byte[] CartSram { get; set; }
        public string CartSramFilePath { get; set; }
    }
}

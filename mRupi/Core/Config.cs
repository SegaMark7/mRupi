namespace mRupi.Core;

public class Config
{
    public class SystemInfo
    {
        public CartInfo Cart { get; set; } = new CartInfo();
        public List<byte> BiosRom { get; set; } = new List<byte>();
        public List<byte> SoundRom { get; set; } = new List<byte>();
    }

    public class CartInfo
    {
        public List<byte> Rom { get; set; } = new List<byte>();
        public List<byte> Sram { get; set; } = new List<byte>();
        public string SramFilePath { get; set; }
    }
}

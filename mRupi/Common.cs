namespace mRupi;

public static class Common
{
    public static ushort Bswap16(ushort value)
    {
        return (ushort)((value >> 8) | (value << 8));
    }

    public static uint Bswap32(uint value)
    {
        return (value >> 24) |
               ((value >> 8) & 0xFF00) |
               ((value << 8) & 0xFF0000) |
               (value << 24);
    }
}

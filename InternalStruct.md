������� �������� ��������� ������ ����, ������� �������� �� �� � ����� ��������� ����� ���� little endian ��� big endian.

�������� SRAM ���������� �� ������ 0x10 ��� ����� SRAM � 0x14 ��� �����.

���� SRAM ���� ��� ��������, �� ��� ������ ������� SRAM, �������������������� ����� ����� 0xFF.
���� ���� ��� ������, �� ������ ������� ����� ���������

# ROM

������ �� ���� �������������� �� �������� �

```cs
private static void MapPagetable(List<byte[]> table, byte[] data, uint start, uint size)
{
    start >>= 12;
    size >>= 12;

    for (uint i = 0; i < size; i++)
    {
        table[(int)(start + i)] = data[(int)(i << 12)..];
    }
}
```

����� ����������� ����� �������

```cs
public static byte[][]? GetSh2Pagetable()
{
    return state?.Sh2Pagetable.ToArray();
}
```

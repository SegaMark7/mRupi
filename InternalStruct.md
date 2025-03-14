Сначало эмулятор считывает первый байт, который отвечает за то в какой кодировке будет игра little endian или big endian.

Значение SRAM находяться по адресу 0x10 это старт SRAM и 0x14 это конец.

Если SRAM файл был загружен, но был меньше размера SRAM, неинициализированные байты будут 0xFF.
Если файл был больше, то размер массива будет ограничен

# ROM

данные из рома распределяются по таблицам в

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

потом считываются через функцию

```cs
public static byte[][]? GetSh2Pagetable()
{
    return state?.Sh2Pagetable.ToArray();
}
```

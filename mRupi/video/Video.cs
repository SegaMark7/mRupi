using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mRupi.video
{
    class Video
    {
        private Bitmap displayOutput;

        private Color ConvertColor(ushort color)
        {
            // Конвертируем 16-bit цвет в RGB
            int r = ((color >> 10) & 0x1F) << 3;
            int g = ((color >> 5) & 0x1F) << 3;
            int b = (color & 0x1F) << 3;
            return Color.FromArgb(r, g, b);
        }

        public void RenderBitmap()
        {
            // Рендерим bitmap данные
            //for (int y = 0; y < DISPLAY_HEIGHT; y++)
            //{
            //    for (int x = 0; x < DISPLAY_WIDTH; x++)
            //    {
            //        int index = y * DISPLAY_WIDTH + x;
            //        if (index * 2 + 1 < bitmapVram.Length)
            //        {
            //            ushort color = (ushort)((bitmapVram[index * 2 + 1] << 8) | bitmapVram[index * 2]);
            //            displayOutput.SetPixel(x, y, ConvertColor(color));
            //        }
            //    }
            //}
        }

        public Bitmap GetDisplayOutput()
        {
            return displayOutput;
        }
    }
}

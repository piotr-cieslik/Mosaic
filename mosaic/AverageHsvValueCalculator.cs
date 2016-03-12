using System;
using System.Drawing;

namespace mosaic
{
    internal static class AverageHsvValueCalculator
    {
        public static decimal Calculate(Image image)
        {
            using (var bitmap = new Bitmap(image))
            {
                var sum = 0.0m;
                for (int x = 0; x < bitmap.Width; x++)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        var pixel = bitmap.GetPixel(x, y);
                        var max = Math.Max(pixel.R, Math.Max(pixel.G, pixel.B));
                        sum += max;
                    }
                }
                var pixels = bitmap.Width * bitmap.Height;
                var result = sum / pixels / 255.0m;
                return result;
            }
        }
    }
}
using System;
using System.Drawing;

namespace mosaic
{
    internal static class AverageHsvValueCalculator
    {
        public static int Calculate(Bitmap image)
        {
            var sum = 0.0f;
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    var max = Math.Max(pixel.R, Math.Max(pixel.G, pixel.B));
                    sum += max;
                }
            }
            var pixels = image.Width * image.Height;
            var result = (int)Math.Floor(sum / pixels);
            return result;
        }
    }
}
using mosaic.ColorSpaces;
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
                        var hsv = bitmap.GetPixel(x, y).ToHsv();
                        sum += hsv.V;
                    }
                }
                var pixels = bitmap.Width * bitmap.Height;
                var result = sum / pixels;
                return result;
            }
        }
    }
}
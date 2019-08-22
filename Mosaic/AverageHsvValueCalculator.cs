using Mosaic.ColorSpaces;
using System.Drawing;

namespace Mosaic
{
    internal static class AverageHsvValueCalculator
    {
        public static float Calculate(Image image)
        {
            using (var bitmap = new Bitmap(image))
            {
                var sum = 0.0f;
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
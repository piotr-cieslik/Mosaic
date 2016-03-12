using mosaic.ColorSpaces;
using System.Drawing;

namespace mosaic
{
    internal sealed class TemporaryImage
    {
        private readonly string _path;

        public TemporaryImage(string path, decimal averageHsvValue)
        {
            _path = path;
            AverageHsvValue = averageHsvValue;
        }

        public decimal AverageHsvValue { get; }

        public Image ChangeHueAndSaturation(Hsv targetHsv)
        {
            var image = LoadImage();
            for (var rx = 0; rx < image.Width; rx++)
            {
                for (var ry = 0; ry < image.Width; ry++)
                {
                    var hsv = image.GetPixel(rx, ry).ToHsv();
                    var newHsv = new Hsv(targetHsv.H, targetHsv.S, hsv.V);
                    var newRgb = newHsv.ToRgb();
                    var newColor = Color.FromArgb(255, newRgb.R, newRgb.G, newRgb.B);
                    image.SetPixel(rx, ry, newColor);
                }
            }

            return image;
        }

        private Bitmap LoadImage()
        {
            return new Bitmap(_path);
        }
    }
}
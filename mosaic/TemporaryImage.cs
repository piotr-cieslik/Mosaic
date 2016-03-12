using mosaic.ColorSpaces;
using System.Drawing;
using System.Drawing.Imaging;

namespace mosaic
{
    internal sealed class TemporaryImage
    {
        private readonly string _path;

        public TemporaryImage(string path, float averageHsvValue)
        {
            _path = path;
            AverageHsvValue = averageHsvValue;
        }

        public float AverageHsvValue { get; }

        public Image ChangeHueAndSaturation(Hsv targetHsv)
        {
            var bitmap = LoadImage();
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

            byte bitsPerPixel = 32;
            var size = bitmapData.Stride * bitmapData.Height;
            byte[] data = new byte[size];
            System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, data, 0, size);

            for (int i = 0; i < size; i += bitsPerPixel / 8)
            {
                var b = data[i];
                var g = data[i + 1];
                var r = data[i + 2];

                var hsv = new Rgb(r, g, b).ToHsv();
                var newHsv = new Hsv(targetHsv.H, targetHsv.S, hsv.V);
                var newRgb = newHsv.ToRgb();

                data[i] = newRgb.B;
                data[i + 1] = newRgb.G;
                data[i + 2] = newRgb.R;
            }

            System.Runtime.InteropServices.Marshal.Copy(data, 0, bitmapData.Scan0, data.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }

        private Bitmap LoadImage()
        {
            return new Bitmap(_path);
        }
    }
}
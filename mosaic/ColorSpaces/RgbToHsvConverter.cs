using System;
using System.Drawing;

namespace mosaic.ColorSpaces
{
    internal static class RgbToHsvConverter
    {
        public static Hsv Convert(byte red, byte green, byte blue)
        {
            var r_ = red / 255.0;
            var g_ = green / 255.0;
            var b_ = blue / 255.0;

            var cMax = Math.Max(r_, Math.Max(g_, b_));
            var cMin = Math.Min(r_, Math.Min(g_, b_));
            var d = cMax - cMin;

            var hue = Hue(d, r_, g_, b_, cMax);
            var saturation = Saturation(d, cMax);
            var value = (int)Math.Round(cMax * 100);

            return new Hsv(hue, saturation, value);
        }

        public static Hsv ToHsv(this Color color)
        {
            return Convert(color.R, color.G, color.B);
        }

        private static int Hue(double d, double r_, double g_, double b_, double cMax)
        {
            if (d == 0)
            {
                return 0;
            }

            if (cMax == r_)
            {
                var hue = 0 + ((g_ - b_) * 60) / d;
                if (hue < 0)
                {
                    hue += 360;
                }
                return (int)Math.Round(hue);
            }

            if (cMax == g_)
            {
                var hue = 120 + ((b_ - r_) * 60) / d;
                return (int)Math.Round(hue);
            }

            if (cMax == b_)
            {
                var hue = 240 + ((r_ - g_) * 60) / d;
                return (int)Math.Round(hue);
            }

            throw new NotSupportedException();
        }

        private static int Saturation(double d, double cMax)
        {
            if (cMax == 0)
            {
                return 0;
            }

            var saturation = d / cMax;
            return (int)Math.Round(saturation * 100);
        }
    }
}
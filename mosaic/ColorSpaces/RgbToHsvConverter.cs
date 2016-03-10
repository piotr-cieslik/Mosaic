using System;
using System.Drawing;

namespace mosaic.ColorSpaces
{
    internal static class RgbToHsvConverter
    {
        public static Hsv ToHsv(this Rgb rgb)
        {
            var r_ = rgb.R / 255.0m;
            var g_ = rgb.G / 255.0m;
            var b_ = rgb.B / 255.0m;

            var cMax = Math.Max(r_, Math.Max(g_, b_));
            var cMin = Math.Min(r_, Math.Min(g_, b_));
            var d = cMax - cMin;

            var hue = Hue(d, r_, g_, b_, cMax);
            var saturation = cMax == 0.0m ? 0.0m : d / cMax;
            var value = cMax;

            return new Hsv(hue, saturation, value);
        }

        public static Hsv ToHsv(this Color color)
        {
            return new Rgb(color.R, color.G, color.B).ToHsv();
        }

        private static decimal Hue(decimal d, decimal r_, decimal g_, decimal b_, decimal cMax)
        {
            if (d == 0)
            {
                return 0;
            }

            if (cMax == r_)
            {
                var hue = 0 + ((g_ - b_) * 60m) / d;
                if (hue < 0)
                {
                    hue += 360;
                }
                return hue;
            }

            if (cMax == g_)
            {
                var hue = 120 + ((b_ - r_) * 60m) / d;
                return hue;
            }

            if (cMax == b_)
            {
                var hue = 240 + ((r_ - g_) * 60m) / d;
                return hue;
            }

            throw new NotSupportedException();
        }
    }
}
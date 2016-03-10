using System;

namespace mosaic.ColorSpaces
{
    internal static class HsvToRgbConverter
    {
        public static Rgb GetRgb(decimal r, decimal g, decimal b)
        {
            return new Rgb(ToByte(r), ToByte(g), ToByte(b));
        }

        public static Rgb ToRgb(this Hsv hsv)
        {
            return HsvToRgb(hsv.H, hsv.S, hsv.V);
        }

        private static Rgb HsvToRgb(decimal h, decimal s, decimal v)
        {
            int hi = (int)Math.Floor(h / 60.0m) % 6;
            decimal f = (h / 60.0m) - Math.Floor(h / 60.0m);

            decimal p = v * (1.0m - s);
            decimal q = v * (1.0m - (f * s));
            decimal t = v * (1.0m - ((1.0m - f) * s));

            Rgb ret;

            switch (hi)
            {
                case 0:
                    ret = GetRgb(v, t, p);
                    break;

                case 1:
                    ret = GetRgb(q, v, p);
                    break;

                case 2:
                    ret = GetRgb(p, v, t);
                    break;

                case 3:
                    ret = GetRgb(p, q, v);
                    break;

                case 4:
                    ret = GetRgb(t, p, v);
                    break;

                case 5:
                    ret = GetRgb(v, p, q);
                    break;

                default:
                    ret = new Rgb(0x00, 0x00, 0x00);
                    break;
            }
            return ret;
        }

        private static byte ToByte(decimal value)
        {
            var rounded = Math.Round(value * 1000 * 255) / 1000;
            return (byte)rounded;
        }
    }
}
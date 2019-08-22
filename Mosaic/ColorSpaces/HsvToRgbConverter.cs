using System;

namespace Mosaic.ColorSpaces
{
    internal static class HsvToRgbConverter
    {
        public static Rgb GetRgb(float r, float g, float b)
        {
            return new Rgb(ToByte(r), ToByte(g), ToByte(b));
        }

        public static Rgb ToRgb(this Hsv hsv)
        {
            return HsvToRgb(hsv.H, hsv.S, hsv.V);
        }

        private static Rgb HsvToRgb(float h, float s, float v)
        {
            int hi = (int)Math.Floor(h / 60.0) % 6;
            float f = (h / 60.0f) - (float)Math.Floor(h / 60.0);

            float p = v * (1.0f - s);
            float q = v * (1.0f - (f * s));
            float t = v * (1.0f - ((1.0f - f) * s));

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

        private static byte ToByte(float value)
        {
            var rounded = Math.Round(value * 1000 * 255) / 1000;
            return (byte)rounded;
        }
    }
}
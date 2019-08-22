namespace Mosaic.ColorSpaces
{
    internal struct Hsv
    {
        public Hsv(float hue, float saturation, float value) : this()
        {
            H = hue;
            S = saturation;
            V = value;
        }

        public float H { get; }
        public float S { get; }
        public float V { get; }

        public override string ToString()
        {
            return $"H:{H} S:{S} V:{V}";
        }
    }
}
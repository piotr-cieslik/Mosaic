namespace mosaic.ColorSpaces
{
    internal struct Hsv
    {
        public Hsv(int hue, int saturation, int value) : this()
        {
            H = hue;
            S = saturation;
            V = value;
        }

        public int H { get; }
        public int S { get; }
        public int V { get; }

        public override string ToString()
        {
            return $"H:{H} S:{S} V:{V}";
        }
    }
}
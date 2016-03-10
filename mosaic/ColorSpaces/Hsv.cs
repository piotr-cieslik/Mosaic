namespace mosaic.ColorSpaces
{
    internal struct Hsv
    {
        public Hsv(decimal hue, decimal saturation, decimal value) : this()
        {
            H = hue;
            S = saturation;
            V = value;
        }

        public decimal H { get; }
        public decimal S { get; }
        public decimal V { get; }

        public override string ToString()
        {
            return $"H:{H} S:{S} V:{V}";
        }
    }
}
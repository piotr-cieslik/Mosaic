﻿namespace mosaic.ColorSpaces
{
    internal struct Rgb
    {
        public Rgb(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public byte B { get; }
        public byte G { get; }
        public byte R { get; }
    }
}
namespace mosaic
{
    internal struct ImageMetadata
    {
        public ImageMetadata(string path, int averageHsvValue) : this()
        {
            Path = path;
            AverageHsvValue = averageHsvValue;
        }

        public string Path { get; }

        public int AverageHsvValue { get; }
    }
}
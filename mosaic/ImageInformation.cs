namespace mosaic
{
    internal struct ImageInformation
    {
        public ImageInformation(string name, int averageHsvValue)
        {
            Name = name;
            AverageHsvValue = averageHsvValue;
        }

        public int AverageHsvValue { get; }
        public string Name { get; }
    }
}
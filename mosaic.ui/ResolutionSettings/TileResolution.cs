namespace mosaic.ui.ResolutionSettings
{
    internal struct TileResolution
    {
        public TileResolution(int resolution)
        {
            Resolution = resolution;
        }

        public int Resolution { get; }

        public override string ToString()
        {
            return string.Format("{0} px", Resolution);
        }
    }
}
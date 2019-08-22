namespace Mosaic.Ui.ResolutionSettings
{
    internal struct ImageResolution
    {
        public ImageResolution(int numberOfTilesHorizaontally, int numberOfTilesVertically)
        {
            NumberOfTilesHorizaontally = numberOfTilesHorizaontally;
            NumberOfTilesVertically = numberOfTilesVertically;
        }

        public int NumberOfTilesHorizaontally { get; }
        public int NumberOfTilesVertically { get; }

        public override string ToString()
        {
            return string.Format("{0} x {1}", NumberOfTilesHorizaontally, NumberOfTilesVertically);
        }
    }
}
namespace mosaic
{
    internal sealed class MosaicGenerator
    {
        private IImageProvider _imageProvider;

        public MosaicGenerator(IImageProvider imageProvider)
        {
            _imageProvider = imageProvider;
        }

        public void Generate(string sourceImageFileName, int shortestDimensionSize)
        {
        }
    }
}
namespace mosaic
{
    internal sealed class SourceImagesPreprocesor
    {
        private IImageProvider _sourceImagesProvider;
        private ITemporaryDirectory _temporaryDirectory;

        public SourceImagesPreprocesor(
            IImageProvider sourceImagesProvider,
            ITemporaryDirectory temporaryDirectory)
        {
            _sourceImagesProvider = sourceImagesProvider;
            _temporaryDirectory = temporaryDirectory;
        }

        public void Run()
        {
            var names = _sourceImagesProvider.GetNames();
            foreach (var name in names)
            {
                using (var image = _sourceImagesProvider.GetImage(name))
                {
                    var resizedImage = SquareImageGenerator.Resize(image);
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(resizedImage);
                    var imageInformation = new ImageInformation(name, averageHsvValue);
                    _temporaryDirectory.Save(resizedImage, name);
                    _temporaryDirectory.Save(imageInformation);
                }
            }
        }
    }
}
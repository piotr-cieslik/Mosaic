namespace mosaic
{
    internal sealed class SourceImagesPreprocesor
    {
        private ISourceImagesProvider _sourceImagesProvider;
        private ITemporaryImageInformationStorage _temporaryImageInformationStorage;
        private ITemporaryImageStorage _temporaryImageStorage;

        public SourceImagesPreprocesor(
            ISourceImagesProvider sourceImagesProvider,
            ITemporaryImageStorage temporaryImageStorage,
            ITemporaryImageInformationStorage temporaryImageInformationStorage)
        {
            _sourceImagesProvider = sourceImagesProvider;
            _temporaryImageStorage = temporaryImageStorage;
            _temporaryImageInformationStorage = temporaryImageInformationStorage;
        }

        public void Run()
        {
            var names = _sourceImagesProvider.GetImageNames();
            foreach (var name in names)
            {
                using (var image = _sourceImagesProvider.GetImage(name))
                {
                    var resizedImage = SourceImageResizer.Resize(image);
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(resizedImage);
                    var imageInformation = new ImageInformation(name, averageHsvValue);
                    _temporaryImageStorage.Save(resizedImage, name);
                    _temporaryImageInformationStorage.Save(imageInformation);
                }
            }
        }
    }
}
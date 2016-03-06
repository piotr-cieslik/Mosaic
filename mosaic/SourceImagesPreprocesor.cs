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
            var fileNames = _sourceImagesProvider.GetImageNames();

            foreach (var fileName in fileNames)
            {
                using (var image = _sourceImagesProvider.GetImage(fileName))
                {
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(image);
                    var imageInformation = new ImageInformation(fileName, averageHsvValue);

                    _temporaryImageStorage.Save(image);
                    _temporaryImageInformationStorage.Save(imageInformation);
                }
            }
        }
    }
}
using mosaic.Directories;

namespace mosaic
{
    internal sealed class SourceImagesPreprocesor
    {
        private ISourceDirectory _sourceDirectory;
        private ITemporaryDirectory _temporaryDirectory;

        public SourceImagesPreprocesor(
            ISourceDirectory sourceDirectory,
            ITemporaryDirectory temporaryDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _temporaryDirectory = temporaryDirectory;
        }

        public void Run()
        {
            var names = _sourceDirectory.GetNames();
            foreach (var name in names)
            {
                using (var image = _sourceDirectory.GetImage(name))
                {
                    var resizedImage = SquareImageGenerator.Resize(image);
                    var averageHsvValue = AverageHsvValueCalculator.Calculate(resizedImage);
                    _temporaryDirectory.Save(resizedImage, name, averageHsvValue);
                }
            }
        }
    }
}
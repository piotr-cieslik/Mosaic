using System.IO;

namespace mosaic
{
    internal interface ITemporaryImageInformationStorage
    {
        void Save(ImageInformation imageInformation);
    }

    internal sealed class TemporaryImageInformationStorage : ITemporaryImageInformationStorage
    {
        private string _filePath;

        public TemporaryImageInformationStorage(string temporaryDirectory, string fileName)
        {
            _filePath = Path.Combine(temporaryDirectory, fileName);
        }

        public void Save(ImageInformation imageInformation)
        {
        }
    }
}
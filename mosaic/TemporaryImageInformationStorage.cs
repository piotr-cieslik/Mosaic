using System;

namespace mosaic
{
    internal interface ITemporaryImageInformationStorage
    {
        void Save(ImageInformation imageInformation);
    }

    internal sealed class TemporaryImageInformationStorage : ITemporaryImageInformationStorage
    {
        private string _filePath;

        public TemporaryImageInformationStorage(string filePath)
        {
            _filePath = filePath;
        }

        public void Save(ImageInformation imageInformation)
        {
        }
    }
}
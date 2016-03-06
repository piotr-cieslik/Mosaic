using System;
using System.IO;

namespace mosaic
{
    internal interface ITemporaryImageInformationStorage
    {
        void Save(ImageInformation imageInformation);
    }

    internal sealed class TemporaryImageInformationStorage : ITemporaryImageInformationStorage
    {
        private const string Delimiter = ", ";
        private string _filePath;

        public TemporaryImageInformationStorage(string temporaryDirectory, string fileName)
        {
            _filePath = Path.Combine(temporaryDirectory, fileName);
        }

        public void Save(ImageInformation imageInformation)
        {
            var line = imageInformation.Name + Delimiter + imageInformation.AverageHsvValue + Environment.NewLine;
            File.AppendAllText(_filePath, line);
        }
    }
}
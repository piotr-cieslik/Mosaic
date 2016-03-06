using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace mosaic
{
    internal interface ITemporaryImageInformationStorage
    {
        IReadOnlyCollection<ImageInformation> Get();

        void Save(ImageInformation imageInformation);
    }

    internal sealed class TemporaryImageInformationStorage : ITemporaryImageInformationStorage
    {
        private const char Delimiter = ',';
        private string _filePath;

        public TemporaryImageInformationStorage(string temporaryDirectory, string fileName)
        {
            _filePath = Path.Combine(temporaryDirectory, fileName);
        }

        public IReadOnlyCollection<ImageInformation> Get()
        {
            var information = CreateInformations().ToArray();
            return information;
        }

        public void Save(ImageInformation imageInformation)
        {
            var line = imageInformation.Name + Delimiter + imageInformation.AverageHsvValue + Environment.NewLine;
            File.AppendAllText(_filePath, line);
        }

        private IEnumerable<ImageInformation> CreateInformations()
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var values = line.Split(Delimiter);
                yield return new ImageInformation(values[0], int.Parse(values[1]));
            }
        }
    }
}
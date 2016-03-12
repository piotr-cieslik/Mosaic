using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mosaic.Directories
{
    internal sealed class TemporaryDirectory : ITemporaryDirectory
    {
        public const string ImageInformationsFileName = "_image_information.txt";
        private const char Delimiter = ',';
        private readonly string _filePath;
        private readonly string _temporaryDirectory;

        public TemporaryDirectory(string temporaryDirectory)
        {
            _temporaryDirectory = temporaryDirectory;
            _filePath = Path.Combine(temporaryDirectory, ImageInformationsFileName);
        }

        public IReadOnlyCollection<TemporaryImage> Get()
        {
            var temporaryImages = new List<TemporaryImage>();
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                var values = line.Split(Delimiter);
                var path = Path.Combine(_temporaryDirectory, values[0]);
                var averageHsvValue = decimal.Parse(values[1]);
                temporaryImages.Add(new TemporaryImage(path, averageHsvValue));
            }
            return temporaryImages;
        }

        public void Save(Image image, string name)
        {
            var path = Path.Combine(_temporaryDirectory, Path.ChangeExtension(name, ".png"));
            image.Save(path, ImageFormat.Png);
        }

        public void Save(ImageInformation imageInformation)
        {
            var line = Path.ChangeExtension(imageInformation.Name, ".png") + Delimiter + imageInformation.AverageHsvValue + Environment.NewLine;
            File.AppendAllText(_filePath, line);
        }
    }
}
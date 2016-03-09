using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

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

        public Image Get(string name)
        {
            var path = Path.Combine(_temporaryDirectory, Path.ChangeExtension(name, ".png"));
            return Image.FromFile(path);
        }

        public IReadOnlyCollection<ImageInformation> Get()
        {
            var information = CreateInformations().ToArray();
            return information;
        }

        public void Save(Image image, string name)
        {
            var path = Path.Combine(_temporaryDirectory, Path.ChangeExtension(name, ".png"));
            image.Save(path, ImageFormat.Png);
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
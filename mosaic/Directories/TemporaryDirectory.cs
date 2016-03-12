﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
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
                var averageHsvValue = float.Parse(values[1], CultureInfo.InvariantCulture);
                temporaryImages.Add(new TemporaryImage(path, averageHsvValue));
            }
            return temporaryImages;
        }

        public void Save(Image image, string name, float averageHsvValue)
        {
            var temporaryFileName = Path.ChangeExtension(name, ".png");
            var path = Path.Combine(_temporaryDirectory, temporaryFileName);
            image.Save(path, ImageFormat.Png);

            var line = temporaryFileName + Delimiter + averageHsvValue.ToString(CultureInfo.InvariantCulture) + Environment.NewLine;
            File.AppendAllText(_filePath, line);
        }
    }
}
using mosaic.ColorSpaces;
using mosaic.Directories;
using System;
using System.Drawing;
using System.Linq;

namespace mosaic
{
    internal sealed class MosaicGenerator
    {
        private readonly IOutputDirectory _outputDirectory;
        private readonly ISourceDirectory _sourceDirectory;
        private readonly ITemporaryDirectory _temporaryDirectory;

        public MosaicGenerator(
            ISourceDirectory sourceDirectory,
            ITemporaryDirectory temporaryDirectory,
            IOutputDirectory outputDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _temporaryDirectory = temporaryDirectory;
            _outputDirectory = outputDirectory;
        }

        public void Generate(string sourceImageFileName, int width, int height, int tileSize)
        {
            var sourceImage = _sourceDirectory.GetImage(sourceImageFileName);
            var sourceImageSmall = new Bitmap(Resize(sourceImage, width, height));

            var outputImage = new Bitmap(width * tileSize, height * tileSize);
            var temporaryImages = _temporaryDirectory.Get();
            var sourceArea = new Rectangle(0, 0, tileSize, tileSize);

            using (var g = Graphics.FromImage(outputImage))
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var sourcePixelHsvValues = sourceImageSmall.GetPixel(x, y).ToHsv();
                        var temporaryImage = temporaryImages.First();
                        using (var tile = temporaryImage.ChangeHueAndSaturation(sourcePixelHsvValues))
                        {
                            var targetArea = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                            g.DrawImage(tile, targetArea, sourceArea, GraphicsUnit.Pixel);
                        }
                    }
                    Console.WriteLine(x * 1.0 / width);
                }
            }

            _outputDirectory.Save(outputImage);
        }

        private Image Resize(Image sourceImage, int width, int height)
        {
            var outputImage = new Bitmap(width, height);
            using (var g = Graphics.FromImage(outputImage))
            {
                var outputArea = new Rectangle(0, 0, width, height);
                var sourceArea = new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
                g.DrawImage(sourceImage, outputArea, sourceArea, GraphicsUnit.Pixel);
            }
            return outputImage;
        }
    }
}
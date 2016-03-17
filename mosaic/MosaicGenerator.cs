using mosaic.ColorSpaces;
using mosaic.Directories;
using System;
using System.Drawing;

namespace mosaic
{
    public sealed class MosaicGenerator
    {
        private readonly IOutputDirectory _outputDirectory;
        private readonly ISourceDirectory _sourceDirectory;

        public MosaicGenerator(
            ISourceDirectory sourceDirectory,
            IOutputDirectory outputDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _outputDirectory = outputDirectory;
        }

        public void Generate(string basicImagePath, int width, int height, int tileSize)
        {
            var basicImage = Image.FromFile(basicImagePath);
            var basicImageSamll = new Bitmap(Resize(basicImage, width, height));

            var tilesCollection = new TilesCollection(_sourceDirectory);
            tilesCollection.Fill(tileSize);

            var outputImage = new Bitmap(width * tileSize, height * tileSize);
            var sourceArea = new Rectangle(0, 0, tileSize, tileSize);

            using (var g = Graphics.FromImage(outputImage))
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var hsv = basicImageSamll.GetPixel(x, y).ToHsv();
                        var temporaryImage = tilesCollection.FindTileSimilarToColor(hsv);
                        using (var tile = temporaryImage.ChangeHueAndSaturation(hsv))
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
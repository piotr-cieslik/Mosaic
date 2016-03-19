using mosaic.ColorSpaces;
using mosaic.Directories;
using System.Drawing;

namespace mosaic
{
    public sealed class MosaicGenerator
    {
        private readonly IOutputDirectory _outputDirectory;
        private readonly IProgressNotificator _progressNotificator;
        private readonly ISourceDirectory _sourceDirectory;

        public MosaicGenerator(
            ISourceDirectory sourceDirectory,
            IOutputDirectory outputDirectory,
            IProgressNotificator progressNotificator)
        {
            _sourceDirectory = sourceDirectory;
            _outputDirectory = outputDirectory;
            _progressNotificator = progressNotificator;
        }

        public void Generate(string basicImagePath, int width, int height, int tileSize)
        {
            var basicImage = Image.FromFile(basicImagePath);
            var basicImageSamll = new Bitmap(Resize(basicImage, width, height));

            var tilesCollection = new TilesCollection(_sourceDirectory, _progressNotificator);
            tilesCollection.Fill(tileSize);

            var outputImage = new Bitmap(width * tileSize, height * tileSize);
            var sourceArea = new Rectangle(0, 0, tileSize, tileSize);

            var totalTiles = width * height;
            var processedTiles = 0;
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
                        processedTiles++;
                        _progressNotificator.NotifyGeneratingProgress(processedTiles, totalTiles);
                    }
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
using mosaic.ColorSpaces;
using mosaic.Directories;
using System;
using System.Drawing;

namespace mosaic
{
    internal sealed class MosaicGenerator
    {
        private readonly IOutputDirectory _outputDirectory;
        private readonly ITemporaryDirectory _temporaryDirectory;
        private readonly ISourceDirectory _sourceDirectory;
        private readonly SourceImagesPreprocesor _sourceImagePreprocesor;

        public MosaicGenerator(
            ISourceDirectory sourceDirectory,
            ITemporaryDirectory temporaryDirectory,
            IOutputDirectory outputDirectory)
        {
            _sourceDirectory = sourceDirectory;
            _temporaryDirectory = temporaryDirectory;
            _outputDirectory = outputDirectory;

            _sourceImagePreprocesor = new SourceImagesPreprocesor(
                sourceDirectory,
                temporaryDirectory);
        }

        public void Generate(Image basicImage, int width, int height, int tileSize)
        {
            var basicImageSamll = new Bitmap(Resize(basicImage, width, height));
            var outputImage = new Bitmap(width * tileSize, height * tileSize);
            var temporaryImages = _temporaryDirectory.Get();
            var sourceArea = new Rectangle(0, 0, tileSize, tileSize);

            _sourceImagePreprocesor.Run();

            var finder = new SimilarTemporaryImageFinder(temporaryImages);
            using (var g = Graphics.FromImage(outputImage))
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var hsv = basicImageSamll.GetPixel(x, y).ToHsv();
                        var temporaryImage = finder.Find(hsv);
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
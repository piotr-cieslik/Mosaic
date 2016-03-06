using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mosaic
{
    internal sealed class ImageSizeNormalizer
    {
        private readonly IImageSizeNormalizerProgressMonitor _progressMonitor;

        public ImageSizeNormalizer(IImageSizeNormalizerProgressMonitor progressMonitor)
        {
            _progressMonitor = progressMonitor;
        }

        public void Normalize(string sourceDirectoryPath, string outputDirectoryPath, int edgeSize)
        {
            CleanOutputDirectory(outputDirectoryPath);
            var outputArea = new Rectangle(0, 0, edgeSize, edgeSize);
            var paths = Directory.GetFiles(sourceDirectoryPath, "*.jpg");
            foreach (var path in paths)
            {
                using (var sourceImage = Image.FromFile(path))
                {
                    FixOrientationBasedOnExifInformation(sourceImage);

                    var shortestEdge = Math.Min(sourceImage.Height, sourceImage.Width);
                    var verticalDifference = sourceImage.Height - sourceImage.Width;
                    var horizontalDifference = sourceImage.Width - sourceImage.Height;

                    {
                        var outputFilePath = outputDirectoryPath + Path.GetFileNameWithoutExtension(path) + ".png";
                        var sourceArea = new Rectangle(0, 0, shortestEdge, shortestEdge);
                        CreateAndSave(sourceImage, sourceArea, outputArea, outputFilePath);
                    }

                    if (verticalDifference > 0)
                    {
                        var outputFilePath = outputDirectoryPath + Path.GetFileNameWithoutExtension(path) + "_v.png";
                        var sourceArea = new Rectangle(0, verticalDifference, shortestEdge, shortestEdge);
                        CreateAndSave(sourceImage, sourceArea, outputArea, outputFilePath);
                    }

                    if (horizontalDifference > 0)
                    {
                        var outputFilePath = outputDirectoryPath + Path.GetFileNameWithoutExtension(path) + "_h.png";
                        var sourceArea = new Rectangle(horizontalDifference, 0, shortestEdge, shortestEdge);
                        CreateAndSave(sourceImage, sourceArea, outputArea, outputFilePath);
                    }
                }
            }
        }

        private static void CleanOutputDirectory(string outputDirectoryPath)
        {
            if (Directory.Exists(outputDirectoryPath))
            {
                Directory.Delete(outputDirectoryPath, true);
            }
            Directory.CreateDirectory(outputDirectoryPath);
        }

        private static void CreateAndSave(Image sourceImage, Rectangle sourceArea, Rectangle outputArea, string outputFilePath)
        {
            using (var outputImage = new Bitmap(outputArea.Width, outputArea.Height))
            {
                using (var graphic = Graphics.FromImage(outputImage))
                {
                    graphic.DrawImage(sourceImage, outputArea, sourceArea, GraphicsUnit.Pixel);
                }

                outputImage.Save(outputFilePath, ImageFormat.Png);
            }
        }

        private static void FixOrientationBasedOnExifInformation(Image image)
        {
            if (Array.IndexOf(image.PropertyIdList, 274) > -1)
            {
                var orientation = (int)image.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;

                    case 2:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;

                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;

                    case 4:
                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;

                    case 5:
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;

                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;

                    case 7:
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;

                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                // This EXIF data is now invalid and should be removed.
                image.RemovePropertyItem(274);
            }
        }
    }
}
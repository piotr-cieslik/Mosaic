using System;
using System.Drawing;

namespace mosaic
{
    internal static class SquareImageGenerator
    {
        private const int OutputImageResolution = 100;
        private static readonly Rectangle _outputArea = new Rectangle(0, 0, OutputImageResolution, OutputImageResolution);

        public static Image Resize(Image sourceImage)
        {
            var sourceArea = CreateSourceArea(sourceImage);
            var outputImage = new Bitmap(_outputArea.Width, _outputArea.Height);
            using (var graphic = Graphics.FromImage(outputImage))
            {
                graphic.DrawImage(sourceImage, _outputArea, sourceArea, GraphicsUnit.Pixel);
            }
            FixOutputImageOrientationBasedOnSourceExifInformation(sourceImage, outputImage);
            return outputImage;
        }

        private static Rectangle CreateSourceArea(Image sourceImage)
        {
            var shortestEdge = Math.Min(sourceImage.Height, sourceImage.Width);

            if (sourceImage.Width > sourceImage.Height)
            {
                var xStart = (sourceImage.Width - sourceImage.Height) / 2;
                return new Rectangle(xStart, 0, shortestEdge, shortestEdge);
            }

            if (sourceImage.Width < sourceImage.Height)
            {
                var yStart = (sourceImage.Height - sourceImage.Width) / 2;
                return new Rectangle(0, yStart, shortestEdge, shortestEdge);
            }

            return new Rectangle(0, 0, sourceImage.Width, sourceImage.Height);
        }

        private static void FixOutputImageOrientationBasedOnSourceExifInformation(Image sourceImage, Image outputImage)
        {
            if (Array.IndexOf(sourceImage.PropertyIdList, 274) > -1)
            {
                var orientation = (int)sourceImage.GetPropertyItem(274).Value[0];
                switch (orientation)
                {
                    case 1:
                        // No rotation required.
                        break;

                    case 2:
                        outputImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;

                    case 3:
                        outputImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;

                    case 4:
                        outputImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;

                    case 5:
                        outputImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;

                    case 6:
                        outputImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;

                    case 7:
                        outputImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;

                    case 8:
                        outputImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }
            }
        }
    }
}
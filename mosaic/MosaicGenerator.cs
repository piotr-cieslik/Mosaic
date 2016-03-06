using System.Drawing;
using System.Linq;

namespace mosaic
{
    internal sealed class MosaicGenerator
    {
        private readonly IImageProvider _imageProvider;
        private readonly IMosaicPersister _mosaicPersister;
        private readonly ITemporaryImageInformationStorage _temporaryImageInformationStorage;
        private readonly ITemporaryImageStorage _temporaryImageStorage;

        public MosaicGenerator(
            IImageProvider imageProvider,
            ITemporaryImageStorage temporaryImageStorage,
            ITemporaryImageInformationStorage temporaryImageInformationStorage,
            IMosaicPersister mosaicPersister)
        {
            _imageProvider = imageProvider;
            _temporaryImageStorage = temporaryImageStorage;
            _temporaryImageInformationStorage = temporaryImageInformationStorage;
            _mosaicPersister = mosaicPersister;
        }

        public void Generate(string sourceImageFileName, int width, int height, int tileSize)
        {
            var sourceImage = _imageProvider.GetImage(sourceImageFileName);
            var sourceImageSmall = Resize(sourceImage, width, height);

            var outputImage = new Bitmap(width * tileSize, height * tileSize);
            var information = _temporaryImageInformationStorage.Get();
            var image = _temporaryImageStorage.Get(information.First().Name);
            var resizedImage = Resize(image, tileSize, tileSize);
            var sourceArea = new Rectangle(0, 0, tileSize, tileSize);

            using (var g = Graphics.FromImage(outputImage))
            {
                for (var x = 0; x < width; x++)
                {
                    for (var y = 0; y < height; y++)
                    {
                        var targetArea = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                        g.DrawImage(resizedImage, targetArea, sourceArea, GraphicsUnit.Pixel);
                    }
                }
            }

            _mosaicPersister.Save(outputImage);
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
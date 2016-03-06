using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mosaic
{
    internal interface ITemporaryImageStorage
    {
        void Save(Image image, string name);
    }

    internal sealed class TemporaryImageStorage : ITemporaryImageStorage
    {
        private string _temporaryDirectory;

        public TemporaryImageStorage(string temporaryDirectory)
        {
            _temporaryDirectory = temporaryDirectory;
        }

        public void InitializeStorage()
        {
            if (Directory.Exists(_temporaryDirectory))
            {
                Directory.Delete(_temporaryDirectory, true);
            }
            Directory.CreateDirectory(_temporaryDirectory);
        }

        public void Save(Image image, string name)
        {
            var path = Path.Combine(_temporaryDirectory, Path.ChangeExtension(name, ".png"));
            image.Save(path, ImageFormat.Png);
        }
    }
}
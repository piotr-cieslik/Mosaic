using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mosaic
{
    internal interface IMosaicPersister
    {
        void Save(Image image);
    }

    internal sealed class MosaicPersister : IMosaicPersister
    {
        private readonly string _outputDirectory;

        public MosaicPersister(string outputDirectory)
        {
            _outputDirectory = outputDirectory;
        }

        public void Save(Image image)
        {
            var name = "mosaic_" + DateTime.Now.ToString("yyy-MM-dd HH-MM-ss") + ".jpg";
            image.Save(Path.Combine(_outputDirectory, name), ImageFormat.Jpeg);
        }
    }
}
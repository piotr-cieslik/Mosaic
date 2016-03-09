using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace mosaic.Directories
{
    internal sealed class OutputDirectory : IOutputDirectory
    {
        private readonly string _outputDirectory;

        public OutputDirectory(string outputDirectory)
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
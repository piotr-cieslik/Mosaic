using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mosaic
{
    internal interface IImageProvider
    {
        Image GetImage(string name);

        IReadOnlyCollection<string> GetNames();
    }

    internal sealed class ImageProvider : IImageProvider
    {
        private readonly string _sourceDirectoryPath;

        public ImageProvider(string sourceDirectoryPath)
        {
            _sourceDirectoryPath = sourceDirectoryPath;
        }

        public Image GetImage(string name)
        {
            var path = Path.Combine(_sourceDirectoryPath, name);
            return Image.FromFile(path);
        }

        public IReadOnlyCollection<string> GetNames()
        {
            var paths = Directory.GetFiles(_sourceDirectoryPath, "*.jpg");
            return paths.Select(path => Path.GetFileName(path)).ToArray();
        }
    }
}
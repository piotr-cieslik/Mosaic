using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mosaic
{
    internal interface ISourceImagesProvider
    {
        Image GetImage(string name);

        IReadOnlyCollection<string> GetImageNames();
    }

    internal sealed class SourceImagesProvider : ISourceImagesProvider
    {
        private readonly string _sourceDirectoryPath;

        public SourceImagesProvider(string sourceDirectoryPath)
        {
            _sourceDirectoryPath = sourceDirectoryPath;
        }

        public Image GetImage(string name)
        {
            var path = Path.Combine(_sourceDirectoryPath, name);
            return Image.FromFile(path);
        }

        public IReadOnlyCollection<string> GetImageNames()
        {
            var paths = Directory.GetFiles(_sourceDirectoryPath, "*.jpg");
            return paths.Select(path => Path.GetFileName(path)).ToArray();
        }
    }
}
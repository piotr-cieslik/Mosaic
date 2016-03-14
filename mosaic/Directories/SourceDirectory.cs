using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace mosaic.Directories
{
    public sealed class SourceDirectory : ISourceDirectory
    {
        private readonly string _sourceDirectoryPath;

        public SourceDirectory(string sourceDirectoryPath)
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
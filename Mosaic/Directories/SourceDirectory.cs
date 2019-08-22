using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Mosaic.Directories
{
    public sealed class SourceDirectory : ISourceDirectory
    {
        private readonly IReadOnlyCollection<string> _sourceDirectoryPaths;

        public SourceDirectory(IReadOnlyCollection<string> sourceDirectoryPaths)
        {
            _sourceDirectoryPaths = sourceDirectoryPaths;
        }

        public Image GetImage(string path)
        {
            return Image.FromFile(path);
        }

        public IReadOnlyCollection<string> GetPaths()
        {
            return _sourceDirectoryPaths.SelectMany(p => Directory.GetFiles(p, "*.jpg")).ToArray();
        }
    }
}
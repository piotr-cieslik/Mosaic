using System.Collections.Generic;
using System.Drawing;

namespace Mosaic.Directories
{
    public interface ISourceDirectory
    {
        Image GetImage(string path);

        IReadOnlyCollection<string> GetPaths();
    }
}
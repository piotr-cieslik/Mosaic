using System.Collections.Generic;
using System.Drawing;

namespace mosaic.Directories
{
    internal interface ISourceDirectory
    {
        Image GetImage(string name);

        IReadOnlyCollection<string> GetNames();
    }
}
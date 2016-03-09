using System.Drawing;

namespace mosaic.Directories
{
    internal interface IOutputDirectory
    {
        void Save(Image image);
    }
}
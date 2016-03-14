using System.Drawing;

namespace mosaic.Directories
{
    public interface IOutputDirectory
    {
        void Save(Image image);
    }
}
using System.Drawing;

namespace Mosaic.Directories
{
    public interface IOutputDirectory
    {
        void Save(Image image);
    }
}
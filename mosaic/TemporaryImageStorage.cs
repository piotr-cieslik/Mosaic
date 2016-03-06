using System.Drawing;

namespace mosaic
{
    internal interface ITemporaryImageStorage
    {
        void Save(Image image);
    }

    internal sealed class TemporaryImageStorage : ITemporaryImageStorage
    {
        public void Save(Image image)
        {
        }
    }
}
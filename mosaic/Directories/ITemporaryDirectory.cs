using System.Collections.Generic;
using System.Drawing;

namespace mosaic.Directories
{
    public interface ITemporaryDirectory
    {
        IReadOnlyCollection<TemporaryImage> Get();

        void Save(Image image, string name, float averageHsvValue);
    }
}
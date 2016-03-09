using System.Collections.Generic;
using System.Drawing;

namespace mosaic.Directories
{
    internal interface ITemporaryDirectory
    {
        Image Get(string name);

        IReadOnlyCollection<ImageInformation> Get();

        void Save(Image image, string name);

        void Save(ImageInformation imageInformation);
    }
}
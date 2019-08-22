using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.BaseImageSelection
{
    internal sealed class BaseImageSelected : IMessage
    {
        public BaseImageSelected(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
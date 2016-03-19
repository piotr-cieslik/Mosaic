using mosaic.ui.EventAggregation;

namespace mosaic.ui.BaseImageSelection
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
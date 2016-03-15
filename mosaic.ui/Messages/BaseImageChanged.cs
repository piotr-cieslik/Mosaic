using EventAggregator;

namespace mosaic.ui.Messages
{
    internal sealed class BaseImageChanged : IMessage
    {
        public BaseImageChanged(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
using EventAggregator;

namespace mosaic.ui.Messages
{
    internal sealed class SourceDirectoryAdded : IMessage
    {
        public SourceDirectoryAdded(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
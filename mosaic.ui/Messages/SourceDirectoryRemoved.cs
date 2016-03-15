using EventAggregator;

namespace mosaic.ui.Messages
{
    internal sealed class SourceDirectoryRemoved : IMessage
    {
        public SourceDirectoryRemoved(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
using mosaic.ui.EventAggregation;

namespace mosaic.ui.SourceDirectoriesSelection
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
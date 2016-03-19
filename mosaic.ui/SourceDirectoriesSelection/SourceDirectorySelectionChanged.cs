using mosaic.ui.EventAggregation;

namespace mosaic.ui.SourceDirectoriesSelection
{
    internal sealed class SourceDirectorySelectionChanged : IMessage
    {
        public SourceDirectorySelectionChanged(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
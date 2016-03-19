using mosaic.ui.EventAggregation;

namespace mosaic.ui.SourceDirectoriesSelection
{
    internal sealed class SourceDirectorySelected : IMessage
    {
        public SourceDirectorySelected(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
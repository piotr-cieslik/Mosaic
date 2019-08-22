using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.SourceDirectoriesSelection
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
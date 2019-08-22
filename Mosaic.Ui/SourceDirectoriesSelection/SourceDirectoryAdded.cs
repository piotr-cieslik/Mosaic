using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.SourceDirectoriesSelection
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
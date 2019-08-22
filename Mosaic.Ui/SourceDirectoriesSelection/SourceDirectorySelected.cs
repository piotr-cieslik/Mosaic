using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.SourceDirectoriesSelection
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
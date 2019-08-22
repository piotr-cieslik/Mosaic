using Mosaic.Ui.EventAggregation;

namespace Mosaic.Ui.OutputDirectorySelection
{
    internal sealed class OutputDirectoryChanged : IMessage
    {
        public OutputDirectoryChanged(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
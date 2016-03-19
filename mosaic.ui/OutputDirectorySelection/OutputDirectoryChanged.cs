using mosaic.ui.EventAggregation;

namespace mosaic.ui.OutputDirectorySelection
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
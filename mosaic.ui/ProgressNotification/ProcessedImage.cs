using mosaic.ui.EventAggregation;

namespace mosaic.ui.ProgressNotification
{
    internal sealed class ProcessedImage : IMessage
    {
        public ProcessedImage(int value, int maximum)
        {
            Value = value;
            Maximum = maximum;
        }

        public int Maximum { get; }
        public int Value { get; }
    }
}
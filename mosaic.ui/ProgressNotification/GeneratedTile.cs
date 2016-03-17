using mosaic.ui.EventAggregation;

namespace mosaic.ui.ProgressNotification
{
    internal sealed class GeneratedTile : IMessage
    {
        public GeneratedTile(int value, int maximum)
        {
            Value = value;
            Maximum = maximum;
        }

        public int Maximum { get; }

        public int Value { get; }
    }
}
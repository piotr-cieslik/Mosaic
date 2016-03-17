using mosaic.ui.EventAggregation;

namespace mosaic.ui.ProgressNotification
{
    internal sealed class ProgressNotificator : IProgressNotificator
    {
        private readonly EventAggregator _eventAggregator;

        public ProgressNotificator(EventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public void NotifyGeneratingProgress(int value, int maximum)
        {
            _eventAggregator.Publish(new GeneratedTile(value, maximum));
        }

        public void NotifyPreprocesingProgress(int value, int maximum)
        {
            _eventAggregator.Publish(new ProcessedImage(value, maximum));
        }
    }
}
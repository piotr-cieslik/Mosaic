namespace mosaic.ui.EventAggregation
{
    internal static class EventAggregatorProvider
    {
        private static EventAggregator _eventAgregator = new EventAggregator();

        public static EventAggregator GetInstance()
        {
            return _eventAgregator;
        }
    }
}
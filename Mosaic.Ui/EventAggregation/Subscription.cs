using System;

namespace Mosaic.Ui.EventAggregation
{
    public sealed class Subscription<TMessage> where TMessage : IMessage
    {
        public Subscription(Action<TMessage> action)
        {
            Action = action;
        }

        public Action<TMessage> Action { get; }
    }
}
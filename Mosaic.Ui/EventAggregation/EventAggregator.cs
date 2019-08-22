using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Mosaic.Ui.EventAggregation
{
    public sealed class EventAggregator
    {
        private readonly IDictionary<Type, IList> _subscriptions = new Dictionary<Type, IList>();

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            if (message == null) throw new ArgumentNullException("message");
            var messageType = typeof(TMessage);
            if (_subscriptions.ContainsKey(messageType))
            {
                var subscriptionList = new List<Subscription<TMessage>>(_subscriptions[messageType].Cast<Subscription<TMessage>>());
                foreach (var subscription in subscriptionList)
                {
                    subscription.Action(message);
                }
            }
        }

        public Subscription<TMessage> Subscribe<TMessage>(Action<TMessage> action) where TMessage : IMessage
        {
            var subscription = new Subscription<TMessage>(action);
            var messageType = typeof(TMessage);
            if (_subscriptions.ContainsKey(messageType))
            {
                _subscriptions[messageType].Add(subscription);
            }
            else
            {
                _subscriptions.Add(messageType, new List<Subscription<TMessage>> { subscription });
            }
            return subscription;
        }
    }
}
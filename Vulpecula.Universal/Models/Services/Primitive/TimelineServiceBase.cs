using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class TimelineServiceBase<T> : SuspendableService
    {
        public ObservableCollection<Action<T>> Subscribers { get; }

        // ReSharper disable once MemberCanBeProtected.Global
        public CroudiaProvider Provider { get; }

        protected TimelineServiceBase(CroudiaProvider provider)
        {
            this.Subscribers = new ObservableCollection<Action<T>>();
            this.Provider = provider;
        }

        protected void StartSubscriberRequest()
        {
            Subscribers.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        this.SubscriberAdded((Action<T>)e.NewItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        this.SubscriberRemoved((Action<T>)e.OldItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        SubscriberCleared();
                        break;
                }
            };
        }

        protected virtual void SubscriberAdded(Action<T> obj)
        {
        }

        protected virtual void SubscriberRemoved(Action<T> obj)
        {
        }

        protected virtual void SubscriberCleared()
        {
        }
    }
}
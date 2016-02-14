using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Vulpecula.Universal.Models.Services.Primitive
{
    public abstract class TimelineServiceBase<T> : SuspendableService
    {
        private readonly int _maxStores;
        private readonly List<T> _storedItems;
        protected List<IDisposable> Disposables { get; }
        public ObservableCollection<Action<T>> Subscribers { get; }

        // ReSharper disable once MemberCanBeProtected.Global
        public CroudiaProvider Provider { get; }

        public ReadOnlyCollection<T> StoredItems => _storedItems.AsReadOnly();

        protected TimelineServiceBase(CroudiaProvider provider, int maxStore = 100)
        {
            Subscribers = new ObservableCollection<Action<T>>();
            Provider = provider;
            Disposables = new List<IDisposable>();
            _storedItems = new List<T>();
            _maxStores = maxStore;

            Subscribers.Add(Store);
        }

        protected void StartSubscriberRequest()
        {
            Subscribers.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        SubscriberAdded((Action<T>) e.NewItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        SubscriberRemoved((Action<T>) e.OldItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        SubscriberCleared();
                        break;
                }
            };
        }

        protected virtual void SubscriberAdded(Action<T> obj) {}

        protected virtual void SubscriberRemoved(Action<T> obj) {}

        protected virtual void SubscriberCleared() {}

        private void Store(T item)
        {
            _storedItems.Add(item);
            if (_storedItems.Count > _maxStores)
                _storedItems.RemoveAt(0);
        }

        public void Clear()
        {
            Disposables.ForEach(w => w.Dispose());
            Subscribers.Clear();
            Subscribers.Add(Store);
        }
    }
}
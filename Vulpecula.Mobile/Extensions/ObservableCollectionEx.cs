using System;
using System.Collections.Specialized;
using System.Reactive;
using System.Reactive.Linq;

namespace Vulpecula.Mobile.Extensions
{
    public static class ObservableCollectionEx
    {
        public static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> ToObservable(this INotifyCollectionChanged obj)
        {
            var observable = Observable
            .FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(h =>
                                                                                                     new NotifyCollectionChangedEventHandler
                                                                                                     (h),
                                                                                                     h => obj.CollectionChanged += h,
                                                                                                     h => obj.CollectionChanged -= h);
            return observable;
        }
    }
}
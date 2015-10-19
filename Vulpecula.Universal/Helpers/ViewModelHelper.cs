using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Vulpecula.Universal.Extensions;

namespace Vulpecula.Universal.Helpers
{
    public static class ViewModelHelper
    {
        // 名前なんとかしたほうがヨクナイ？
        public static IDisposable SubscribeNotifyCollectionChanged<T>(INotifyCollectionChanged source, ObservableCollection<T> target)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            var disposable = source.ToObservable().Subscribe(w =>
            {
                switch (w.EventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        target.Insert(w.EventArgs.NewStartingIndex, (T) w.EventArgs.NewItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        target.Remove((T) w.EventArgs.NewItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        target[w.EventArgs.NewStartingIndex] = (T) w.EventArgs.NewItems[0];
                        break;

                    case NotifyCollectionChangedAction.Move:
                        target.Move(w.EventArgs.OldStartingIndex, w.EventArgs.NewStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        target.Clear();
                        break;
                }
            });
            return disposable;
        }

        public static IDisposable SubscribeNotifyCollectionChanged<TModel, TViewModel>(INotifyCollectionChanged source, ObservableCollection<TViewModel> target, Func<TModel, TViewModel> castFunc)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (target == null)
                throw new ArgumentNullException(nameof(target));

            if (castFunc == null)
                throw new ArgumentNullException(nameof(castFunc));

            var disposable = source.ToObservable().Subscribe(w =>
            {
                switch (w.EventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        target.Insert(w.EventArgs.NewStartingIndex, castFunc.Invoke((TModel) w.EventArgs.NewItems[0]));
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        target.Remove(castFunc.Invoke((TModel) w.EventArgs.NewItems[0]));
                        break;

                    case NotifyCollectionChangedAction.Replace:
                        target[w.EventArgs.NewStartingIndex] = castFunc.Invoke((TModel) w.EventArgs.NewItems[0]);
                        break;

                    case NotifyCollectionChangedAction.Move:
                        target.Move(w.EventArgs.OldStartingIndex, w.EventArgs.NewStartingIndex);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        target.Clear();
                        break;
                }
            });
            return disposable;
        }
    }
}
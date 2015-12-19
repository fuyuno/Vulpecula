using System;
using System.ComponentModel;
using System.Reactive.Linq;

using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.Extensions
{
    public static class ViewModelEx
    {
        public static void AddTo(this IDisposable disposable, ViewModel obj)
        {
            obj.CompositeDisposable.Add(disposable);
        }

        public static IDisposable Subscribe(this ViewModel viewModel, string propertyName, Action<PropertyChangedEventArgs> action)
        {
            return Observable.FromEvent<PropertyChangedEventHandler, PropertyChangedEventArgs>(
                handler => (sender, e) => handler(e),
                handler => viewModel.PropertyChanged += handler,
                handler => viewModel.PropertyChanged -= handler)
                .Where(w => w.PropertyName == propertyName)
                .Subscribe(action.Invoke);
        }
    }
}
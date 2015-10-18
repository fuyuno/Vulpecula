using System;

using Vulpecula.Universal.ViewModels.Primitives;

namespace Vulpecula.Universal.Extensions
{
    public static class ViewModelEx
    {
        public static void AddTo(this IDisposable disposable, ViewModel obj)
        {
            obj.CompositeDisposable.Add(disposable);
        }
    }
}
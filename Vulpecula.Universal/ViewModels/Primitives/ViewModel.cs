using System;
using System.Reactive.Disposables;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;

using Microsoft.Practices.ServiceLocation;

using Prism.Windows.AppModel;
using Prism.Windows.Mvvm;

namespace Vulpecula.Universal.ViewModels.Primitives
{
    public class ViewModel : ViewModelBase, IDisposable
    {
        private readonly IResourceLoader _resource;

        public CompositeDisposable CompositeDisposable { get; }
        protected CoreDispatcher Dispatcher { get; }

        protected ViewModel()
        {
            this.CompositeDisposable = new CompositeDisposable();
            this.Dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            this._resource = ServiceLocator.Current.GetInstance<IResourceLoader>();
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this.CompositeDisposable.Dispose();
        }

        public string GetLocalizedString(string key)
        {
            return this._resource.GetString($"{this.GetType().Name}_{key}/Text");
        }
    }
}
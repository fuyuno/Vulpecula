﻿using System;
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
            CompositeDisposable = new CompositeDisposable();
            Dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
            _resource = ServiceLocator.Current.GetInstance<IResourceLoader>();
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public virtual void Dispose()
        {
            CompositeDisposable.Dispose();
        }

        public string GetLocalizedString(string key)
        {
            return _resource.GetString($"{GetType().Name}_{key}/Text");
        }
    }
}
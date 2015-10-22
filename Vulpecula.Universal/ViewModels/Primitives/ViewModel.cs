using System;
using System.Reactive.Disposables;

using Windows.ApplicationModel.Core;
using Windows.UI.Core;

using Prism.Windows.Mvvm;

namespace Vulpecula.Universal.ViewModels.Primitives
{
    public class ViewModel : ViewModelBase, IDisposable
    {
        public CompositeDisposable CompositeDisposable { get; }
        public CoreDispatcher Dispatcher { get; }

        public ViewModel()
        {
            this.CompositeDisposable = new CompositeDisposable();
            this.Dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this.CompositeDisposable.Dispose();
        }
    }
}
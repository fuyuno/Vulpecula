using System;
using System.Reactive.Disposables;

using Prism.Windows.Mvvm;

namespace Vulpecula.Universal.ViewModels.Primitives
{
    public class ViewModel : ViewModelBase, IDisposable
    {
        public CompositeDisposable CompositeDisposable { get; }

        public ViewModel()
        {
            this.CompositeDisposable = new CompositeDisposable();
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
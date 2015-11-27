using System;
using System.Reactive.Disposables;

using Prism.Mvvm;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    public class ViewModel : BindableBase, IDisposable
    {
        protected ILocalization Localization { get; }
        public CompositeDisposable CompositeDisposable { get; }

        protected ViewModel(ILocalization localization)
        {
            this.CompositeDisposable = new CompositeDisposable();
            this.Localization = localization;
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this.CompositeDisposable.Dispose();
        }

        protected string GetLocalizedString(string key)
        {
            return this.Localization.GetString(key);
        }
    }
}
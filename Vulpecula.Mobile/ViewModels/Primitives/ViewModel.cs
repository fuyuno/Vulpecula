using System;
using System.Reactive.Disposables;

using Prism.Mvvm;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    public class ViewModel : BindableBase, IDisposable
    {
        public CompositeDisposable CompositeDisposable { get; }

        protected ILocalization Localization { get; }

        protected ViewModel()
        {
            this.CompositeDisposable = new CompositeDisposable();
            this.Localization = MobileCross.ModelLocator.GetModel<ILocalization>();
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
            return this.Localization.GetString(key);
        }
    }
}
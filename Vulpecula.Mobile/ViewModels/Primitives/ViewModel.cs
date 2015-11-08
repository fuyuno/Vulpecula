using System;
using System.Reactive.Disposables;

using Prism.Mvvm;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    public class ViewModel : BindableBase, IDisposable
    {
        private readonly ILocalization _localization;
        public CompositeDisposable CompositeDisposable { get; }

        protected ViewModel()
        {
            this.CompositeDisposable = new CompositeDisposable();
            this._localization = ApplicationMain.ModelLocator.GetModel<ILocalization>();
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
            return this._localization.GetString(key);
        }
    }
}
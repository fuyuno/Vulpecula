using Prism.Mvvm;

namespace Vulpecula.Universal.ViewModels
{
    internal class MainPageViewModel : BindableBase
    {
        public MainPageViewModel()
        {
            this.Text = "Hello MVVM on UWP!";
        }

        #region Text変更通知プロパティ

        private string _Text;

        public string Text
        {
            get
            { return _Text; }
            set { this.SetProperty(ref this._Text, value); }
        }

        #endregion
    }
}
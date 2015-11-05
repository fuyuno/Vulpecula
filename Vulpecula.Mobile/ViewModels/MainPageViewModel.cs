using JetBrains.Annotations;

using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel
    {
        public MainPageViewModel()
        {
            Message = "Hello Xamarin.Forms with MVVM!";
        }

        #region Message

        private string _message;

        public string Message
        {
            get { return this._message; }
            set { this.SetProperty(ref _message, value); }
        }

        #endregion
    }
}
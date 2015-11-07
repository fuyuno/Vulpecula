using JetBrains.Annotations;

using Vulpecula.Mobile.ViewModels.Primitives;

namespace Vulpecula.Mobile.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel
    {
        public MainPageViewModel()
        {
            Message = this.GetLocalizedString("HelloLabel");
        }

        #region

        private string _message;

        public string Message
        {
            get { return _message; }
            set { this.SetProperty(ref _message, value); }
        }

        #endregion
    }
}
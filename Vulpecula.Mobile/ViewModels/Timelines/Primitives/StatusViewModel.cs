using System;
using System.Windows.Input;

using Prism.Navigation;

using Xamarin.Forms;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Views.Pages;
using Vulpecula.Models;

namespace Vulpecula.Mobile.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly Status _model;
        private readonly Status _originalStatus;

        public StatusViewModel(ILocalization localization, INavigationService navigation, Status status) : base(localization)
        {
            this._navigationService = navigation;
            this._model = status;
            this._originalStatus = this._model.SpreadStatus ?? this._model;
        }

        #region Properties

        public bool IsSpread => this._model.SpreadStatus != null;
        public string SharedMessage => string.Format(this.GetLocalizedString("SharedMessage"), this._model.User.Name.Trim());
        public string ScreenName => $"@{this._originalStatus.User.ScreenName}";
        public string UserName => this._originalStatus.User.Name.Trim().Replace(Environment.NewLine, "");
        public string Text => this._originalStatus.Text.Trim();
        public string Icon => this._originalStatus.User.ProfileImageUrlHttps;
        public string CreatedAt => this._originalStatus.CreatedAt.ToString("HH:mm");
        public string Via => this._originalStatus.Source.Name;

        #endregion

        #region Commands

        private ICommand _onTappedCommand;
        public ICommand OnTappedCommand => _onTappedCommand ?? (_onTappedCommand = new Command(OnTapped));

        private void OnTapped()
        {
            var param = new NavigationParameters();
            param.Add("user", this._originalStatus.User);
            this._navigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion
    }
}
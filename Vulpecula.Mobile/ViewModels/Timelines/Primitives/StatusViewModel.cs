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

        public StatusViewModel(ILocalization localization, INavigationService navigation, Status status) : base(localization)
        {
            this._navigationService = navigation;
            this._model = status;
        }

        #region Properties

        public string ScreenName => $"@{this._model.User.ScreenName}";
        public string UserName => this._model.User.Name.Trim().Replace(Environment.NewLine, "");
        public string Text => this._model.Text.Trim();
        public string Icon => this._model.User.ProfileImageUrlHttps;
        public string CreatedAt => this._model.CreatedAt.ToString("HH:mm");
        public string Via => this._model.Source.Name;

        #endregion

        #region Commands

        private ICommand _onTappedCommand;
        public ICommand OnTappedCommand => _onTappedCommand ?? (_onTappedCommand = new Command(OnTapped));

        private void OnTapped()
        {
            var param = new NavigationParameters();
            param.Add("user", this._model.User);
            this._navigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion
    }
}
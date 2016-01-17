using System;
using System.Windows.Input;

using Prism.Navigation;

using Vulpecula.Mobile.Extensions;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.Views.Pages;
using Vulpecula.Models;

using Xamarin.Forms;

namespace Vulpecula.Mobile.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        private readonly Status _model;
        private readonly INavigationService _navigationService;
        private readonly Status _originalStatus;

        public StatusViewModel(ILocalization localization, INavigationService navigation, Status status) : base(localization)
        {
            _navigationService = navigation;
            _model = status;
            _originalStatus = _model.SpreadStatus ?? _model;
        }

        #region Properties

        public bool IsSpread => _model.SpreadStatus != null;
        public string SharedMessage => string.Format(GetLocalizedString("SharedMessage"), _model.User.Name.ToSingleLine());
        public string ScreenName => $"@{_originalStatus.User.ScreenName}";
        public string UserName => _originalStatus.User.Name.ToSingleLine();
        public string Text => _originalStatus.Text.Trim();
        public string Icon => _originalStatus.User.ProfileImageUrlHttps;
        public bool HasImage => _originalStatus?.Entities?.Media?.MediaUrlHttps != null;

        public string CreatedAt
        {
            get
            {
                var format = "HH:mm";
                // 1 hour
                if (_originalStatus.CreatedAt.AddDays(1) < DateTime.Now)
                    format = "MM/dd HH:mm";
                // 1 year
                if (_originalStatus.CreatedAt.AddYears(1) < DateTime.Now)
                    format = "yy/MM/dd HH:mm";
                return _originalStatus.CreatedAt.ToString(format);
            }
        }

        #endregion

        #region Commands

        #region OnTappedShowUserDetailsCommand

        private ICommand _onTappedShowUserDetailsCommand;

        public ICommand OnTappedShowUserDetailsCommand =>
        _onTappedShowUserDetailsCommand ?? (_onTappedShowUserDetailsCommand = new Command(OnTappedShowUserDetails));

        private void OnTappedShowUserDetails()
        {
            var param = new NavigationParameters();
            param.Add("user", _originalStatus.User);
            _navigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion

        #region OnTappedShowStatusDetailsCommand

        private ICommand _onTappedShowStatusDetailsCommand;

        public ICommand OnTappedShowStatusDetailsCommand =>
        _onTappedShowStatusDetailsCommand ?? (_onTappedShowStatusDetailsCommand = new Command(OnTappedShowStatusDetails));

        private void OnTappedShowStatusDetails()
        {
            var param = new NavigationParameters();
            param.Add("status", _originalStatus);
            _navigationService.Navigate<StatusDetailsPage>(param, false);
        }

        #endregion

        #endregion
    }
}
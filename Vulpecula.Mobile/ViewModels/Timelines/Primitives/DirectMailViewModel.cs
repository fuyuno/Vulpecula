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
    public class DirectMailViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly SecretMail _model;

        public DirectMailViewModel(ILocalization localization, INavigationService navigation, SecretMail status)
            : base(localization)
        {
            this._navigationService = navigation;
            this._model = status;
        }

        #region Properties

        public string ScreenName => $"@{this._model.Sender.ScreenName}";
        public string UserName => this._model.Sender.Name.ToSingleLine();
        public string Text => this._model.Text.Trim();
        public string Icon => this._model.Sender.ProfileImageUrlHttps;
        public string RecipientIcon => this._model.Recipient.ProfileImageUrlHttps;
        public string CreatedAt 
        {
            get {
                var format = "HH:mm";
                // 1 hour
                if(this._model.CreatedAt.AddDays(1) < DateTime.Now)
                {
                    format = "MM/dd HH:mm";
                }
                // 1 year
                if(this._model.CreatedAt.AddYears(1) < DateTime.Now)
                {
                    format = "yy/MM/dd HH:mm";   
                }
                return this._model.CreatedAt.ToString(format);
            }
        }

        #endregion

        #region Commands

        private ICommand _onTappedCommand;
        public ICommand OnTappedCommand => _onTappedCommand ?? (_onTappedCommand = new Command<string>(OnTapped));

        private void OnTapped(string type)
        {
            User user;
            if(type == "Sender")
            {
                user = this._model.Sender;
            }
            else
            {
                user = this._model.Recipient;
            }
            var param = new NavigationParameters();
            param.Add("user", user);
            this._navigationService.Navigate<UserDetailsPage>(param, false);
        }

        #endregion
    }
}


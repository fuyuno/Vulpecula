using System;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Models;

namespace Vulpecula.Mobile.ViewModels.Timelines.Primitives
{
    public class StatusViewModel : ViewModel
    {
        private readonly Status _model;

        public StatusViewModel(ILocalization localization, Status status) : base(localization)
        {
            this._model = status;
        }

        #region Properties

        public string ScreenName => $"@{this._model.User.ScreenName}";
        public string UserName => this._model.User.Name.Replace(Environment.NewLine, "");
        public string Text => this._model.Text.Trim();
        public string Icon => this._model.User.ProfileImageUrlHttps;
        public string CreatedAt => this._model.CreatedAt.ToString("HH:mm");

        #endregion
    }
}
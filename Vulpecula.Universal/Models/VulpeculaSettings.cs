using Prism.Mvvm;
using Windows.Storage;

namespace Vulpecula.Universal.Models
{
    public class VulpeculaSettings : BindableBase
    {
        private readonly ApplicationDataContainer _roamingContainer;

        #region AccessToken

        private string _accessToken;

        public string AccessToken
        {
            get { return this._accessToken; }
            set
            {
                this._roamingContainer.Values[nameof(AccessToken)] = value;
                this.SetProperty(ref this._accessToken, value);
            }
        }

        #endregion

        #region RefreshToken

        private string _refreshToken;

        public string RefreshToken
        {
            get { return this._refreshToken; }
            set
            {
                this._roamingContainer.Values[nameof(RefreshToken)] = value;
                this.SetProperty(ref this._refreshToken, value);
            }
        }

        #endregion

        public VulpeculaSettings()
        {
            this._roamingContainer = ApplicationData.Current.RoamingSettings;
        }
    }
}
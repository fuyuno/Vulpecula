using Windows.Storage;

using Prism.Mvvm;

namespace Vulpecula.Universal.Models
{
    public class VulpeculaSettings : BindableBase
    {
        private readonly ApplicationDataContainer _roamingContainer;

        public VulpeculaSettings()
        {
            this._roamingContainer = ApplicationData.Current.RoamingSettings;
        }
    }
}
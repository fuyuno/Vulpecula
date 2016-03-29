using Vulpecula.Universal.Models;
using Vulpecula.Universal.Services;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels
{
    public class UserAccountViewModel : UserViewModel
    {
        private readonly CroudiaAccount _provider;

        public UserAccountViewModel(CroudiaAccount provider) : base(provider.User, true)
        {
            _provider = provider;
            IsWhisperEnabled = false;
        }

        public void SendWhisper(string text)
        {
            ServiceProvider.RegisterService(new StatusService(_provider, text));
        }

        #region IsWhisperEnabled

        private bool _isWhisperEnabled;

        public bool IsWhisperEnabled
        {
            get { return _isWhisperEnabled; }
            set { SetProperty(ref _isWhisperEnabled, value); }
        }

        #endregion IsWhisperEnabled
    }
}
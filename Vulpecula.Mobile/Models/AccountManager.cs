using System.Collections.ObjectModel;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Models;

namespace Vulpecula.Mobile.Models
{
    /// <summary>
    /// アカウントを管理します。
    /// このクラスは、 Prism により、シングルトンクラスとして管理されます。
    /// </summary>
    [UsedImplicitly]
    public sealed class AccountManager
    {
        private readonly Configuration _configuration;
        private readonly IConstants _constants;

        // Initialize は1度だけ
        private bool _isInit;

        // 現状シングルアカウントなので
        public ObservableCollection<CroudiaProvider> Providers { get; }

        public ObservableCollection<User> Users { get; }

        public AccountManager(Configuration configuration)
        {
            _configuration = configuration;
            Providers = new ObservableCollection<CroudiaProvider>();
            Users = new ObservableCollection<User>();
            _constants = App.ModelLocator.GetModel<IConstants>();
        }

        [UsedImplicitly]
        public void ResetAccounts()
        {
            _configuration.Accounts.Clear();
        }

        public void InitializeAccounts()
        {
            if (_isInit)
                return;
            var vault = App.ModelLocator.GetModel<IPasswordVault>();
            foreach (var account in _configuration.Accounts)
            {
                var credential = vault.FindByUserName(account);
                var provider = new CroudiaProvider(_constants.ConsumerKey, _constants.ConsumerSecret);
                if (!provider.ReAuthorization(credential))
                {
                    _configuration.Accounts.Remove(credential.UserName);
                    continue;
                }
                Providers.Add(provider);
                Users.Add(provider.User);
            }
            _isInit = true;
        }

        public async Task AddAccount(string url)
        {
            var provider = new CroudiaProvider(_constants.ConsumerKey, _constants.ConsumerSecret);
            if (!await provider.Authorization(url))
                return;

            Providers.Add(provider);
            Users.Add(provider.User);
            _configuration.Accounts.Add(provider.User.ScreenName);
        }
    }
}
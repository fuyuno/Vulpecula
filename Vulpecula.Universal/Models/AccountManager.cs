using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using JetBrains.Annotations;

using Vulpecula.Models;
using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models.Dialogs;

namespace Vulpecula.Universal.Models
{
    // TODO: static
    /// <summary>
    /// アカウントを管理します。
    /// </summary>
    public sealed class AccountManager
    {
        private static AccountManager _instance;

        public static AccountManager Instance => _instance ?? (_instance = new AccountManager());

        public ObservableCollection<CroudiaProvider> Providers { get; }

        public ObservableCollection<User> Users { get; }

        private AccountManager()
        {
            Providers = new ObservableCollection<CroudiaProvider>();
            Users = new ObservableCollection<User>();
        }

        [UsedImplicitly]
        public void ResetAccounts()
        {
            try
            {
                var vault = new PasswordVault();
                vault.RetrieveAll();

                var accounts = vault.FindAllByResource(AppDefintions.VulpeculaAppKey);
                foreach (var credential in accounts)
                    vault.Remove(credential);
                Providers.Clear();
                Users.Clear();
            }
            catch (COMException) {}
        }

        public async Task InitializeAccounts()
        {
            try
            {
                var vault = new PasswordVault();
                vault.RetrieveAll();

                var accounts = vault.FindAllByResource(AppDefintions.VulpeculaAppKey);
                foreach (var credential in accounts)
                {
                    var provider = new CroudiaProvider();
                    if (!await provider.Authorization(vault, credential))
                    {
                        // TODO: 再認証処理
                        vault.Remove(credential);
                        continue;
                    }
                    Providers.Add(provider);
                    Users.Add(provider.User);
                }
            }
            catch (COMException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task AuthorizationAccount(PasswordCredential credential = null)
        {
            if (Users.Count >= 10)
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotAdd"), "Error");
                return;
            }

            var provider = new CroudiaProvider();
            if (!await provider.Authorization(new PasswordVault(), credential))
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("FailAuth"), "Error");
                return;
            }
            Providers.Add(provider);
            Users.Add(provider.User);
        }
    }
}
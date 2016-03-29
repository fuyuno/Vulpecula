using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using JetBrains.Annotations;

using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models.Dialogs;

namespace Vulpecula.Universal.Models
{
    // TODO: static
    /// <summary>
    ///     アカウントを管理します。
    /// </summary>
    public sealed class AccountManager
    {
        private readonly ObservableCollection<CroudiaAccount> _accounts;

        public AccountManager()
        {
            _accounts = new ObservableCollection<CroudiaAccount>();
            Accounts = new ReadOnlyObservableCollection<CroudiaAccount>(_accounts);
        }

        public ReadOnlyObservableCollection<CroudiaAccount> Accounts { get; }

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
                _accounts.Clear();
            }
            catch (COMException)
            {
            }
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
                    var account = new CroudiaAccount();
                    if (!await account.Authorization(vault, credential))
                    {
                        // TODO: 再認証処理
                        vault.Remove(credential);
                        continue;
                    }
                    _accounts.Add(account);
                }
            }
            catch (COMException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task AuthorizationAccount(PasswordCredential credential = null)
        {
            if (Accounts.Count >= 10)
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("CanNotAdd"), "Error");
                return;
            }

            var account = new CroudiaAccount();
            if (!await account.Authorization(new PasswordVault(), credential))
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync(LocalizationHelper.GetString("FailAuth"), "Error");
                return;
            }
            _accounts.Add(account);
        }
    }
}
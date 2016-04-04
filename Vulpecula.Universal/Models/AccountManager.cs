using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        private readonly Configuration _configuration;

        public AccountManager(Configuration configuration)
        {
            _configuration = configuration;
            _accounts = new ObservableCollection<CroudiaAccount>();
            _accounts.CollectionChanged += (a, b) => _configuration.AddOrRewriteValue(ConfigurationKeys.UsersKey, _accounts.Select(w => w.User.Id));
            Accounts = new ReadOnlyObservableCollection<CroudiaAccount>(_accounts);
            IsInitialized = false;
        }

        public bool IsInitialized { get; private set; }

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
                var rows = _configuration.GetValue<IEnumerable<long>>(ConfigurationKeys.UsersKey)
                                         .ToList();
                foreach (var credential in accounts)
                {
                    var account = new CroudiaAccount();
                    if (!await account.Authorization(vault, credential))
                    {
                        // TODO: 再認証処理
                        vault.Remove(credential);
                        continue;
                    }
                    account.Row = rows.TakeWhile(w => w != account.User.Id)
                                      .Count();
                    _accounts.Insert(account.Row > _accounts.Count ? 0 : account.Row, account);
                }
            }
            catch (COMException e)
            {
                Debug.WriteLine(e.Message);
            }
            IsInitialized = true;
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
            account.Row = Accounts.Count;
            _accounts.Add(account);
        }
    }
}
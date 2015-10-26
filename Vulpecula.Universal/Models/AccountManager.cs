using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Windows.Security.Credentials;

using JetBrains.Annotations;

using Vulpecula.Models;
using Vulpecula.Universal.Models.Dialogs;

namespace Vulpecula.Universal.Models
{
    /// <summary>
    /// アカウントを管理します。
    /// </summary>
    public class AccountManager
    {
        public ObservableCollection<CroudiaProvider> Providers { get; }

        public ObservableCollection<User> Users { get; }

        public AccountManager()
        {
            this.Providers = new ObservableCollection<CroudiaProvider>();
            this.Users = new ObservableCollection<User>();
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
                this.Providers.Clear();
                this.Users.Clear();
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
                    var provider = new CroudiaProvider();
                    if (!await provider.Authorization(vault, credential))
                    {
                        // TODO: 再認証処理
                        vault.Remove(credential);
                        continue;
                    }
                    this.Providers.Add(provider);
                    this.Users.Add(provider.User);
                }
            }
            catch (COMException e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async Task AuthorizationAccount(PasswordCredential credential = null)
        {
            if (this.Users.Count >= 10)
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync("これ以上アカウントを追加することはできません。", "認証エラー");
                return;
            }

            var provider = new CroudiaProvider();
            if (!await provider.Authorization(new PasswordVault(), credential))
            {
                await MessageDialogWrapper.ShowOkMessageDialogAsync("認証に失敗しました。しばらくしてから、再度実行してください。", "認証エラー");
                return;
            }
            this.Providers.Add(provider);
            this.Users.Add(provider.User);
        }
    }
}
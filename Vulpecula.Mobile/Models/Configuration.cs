using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

using JetBrains.Annotations;

using Vulpecula.Mobile.Extensions;
using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.Models
{
    [UsedImplicitly]
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        // ReSharper disable once InconsistentNaming
        private List<string> _Accounts
        {
            get { return this._configuration.GetArray("Accounts")?.ToList(); }
            set { this._configuration.SetArray("Accounts", value.ToArray()); }
        }

        public ObservableCollection<string> Accounts => new ObservableCollection<string>(this._Accounts);

        public Configuration()
        {
            this._configuration = App.ModelLocator.GetModel<IConfiguration>();
            if (this._Accounts == null)
            {
                this._Accounts = new List<string>();
            }
            this.Accounts.ToObservable().Subscribe(w =>
            {
                List<string> temp;
                switch (w.EventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        temp = this._Accounts;
                        temp.Add(w.EventArgs.NewItems[0].ToString());
                        this._Accounts = new List<string>(temp);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        temp = this._Accounts;
                        temp.Remove(w.EventArgs.NewItems[0].ToString());
                        this._Accounts = new List<string>(temp);
                        break;
                }
            });
        }
    }
}
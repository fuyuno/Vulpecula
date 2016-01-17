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
            get { return _configuration.GetArray("Accounts")?.ToList(); }
            set { _configuration.SetArray("Accounts", value.ToArray()); }
        }

        public ObservableCollection<string> Accounts { get; }

        public Configuration()
        {
            _configuration = App.ModelLocator.GetModel<IConfiguration>();

            if (_Accounts == null)
                _Accounts = new List<string>();
            Accounts = new ObservableCollection<string>(_Accounts);
            Accounts.ToObservable().Subscribe(w =>
            {
                List<string> temp;
                switch (w.EventArgs.Action)
                {
                    case NotifyCollectionChangedAction.Add:
                        temp = _Accounts;
                        temp.Add(w.EventArgs.NewItems[0].ToString());
                        _Accounts = new List<string>(temp);
                        break;

                    case NotifyCollectionChangedAction.Remove:
                        temp = _Accounts;
                        temp.Remove(w.EventArgs.NewItems[0].ToString());
                        _Accounts = new List<string>(temp);
                        break;

                    case NotifyCollectionChangedAction.Reset:
                        _Accounts = new List<string>();
                        break;
                }
            });
        }
    }
}
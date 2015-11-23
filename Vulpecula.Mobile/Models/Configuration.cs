using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.Models
{
    [UsedImplicitly]
    public class Configuration
    {
        private readonly IConfiguration _configuration;

        public List<string> Accounts
        {
            get { return this._configuration.GetArray("Accounts")?.ToList(); }
            private set { this._configuration.SetArray("Accounts", value.ToArray()); }
        }

        public Configuration()
        {
            this._configuration = App.ModelLocator.GetModel<IConfiguration>();
            if (this.Accounts == null)
            {
                this.Accounts = new List<string>();
            }
        }
    }
}
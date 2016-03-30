using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Windows.Storage;

using JetBrains.Annotations;

using Newtonsoft.Json;

using Prism.Mvvm;

using Vulpecula.Universal.Models.Migrate;
using Vulpecula.Universal.Models.Migrate.Primitives;

namespace Vulpecula.Universal.Models
{
    [UsedImplicitly]
    public class Configuration : BindableBase
    {
        private const double ConfigurationVersion = 1.0;

        private readonly List<Migration> _migrations = new List<Migration>
        {
            new Migration(default(double), 1.0, new Migration20160329())
        };

        private readonly ApplicationDataContainer _roamingContainer;

        public Configuration()
        {
            _roamingContainer = ApplicationData.Current.RoamingSettings;
            var oldValue = GetValue<double>(ConfigurationKeys.ConfigurationVersionKey);
            if (oldValue < ConfigurationVersion)
            {
                var updates = _migrations.Where(w => w.IsMigrate(oldValue, ConfigurationVersion));
                Debug.WriteLine("--------------------------- Migration Log ---------------------------");
                foreach (var migration in updates)
                {
                    Debug.WriteLine($"!!!Migrate configuration : revision -> {migration.Rev}");
                    migration.Migrate(this);
                    Debug.WriteLine($"!!!Migration succeeded   : revision -> {migration.Rev}");
                }
            }
            AddOrRewriteValue(ConfigurationKeys.ConfigurationVersionKey, ConfigurationVersion);
        }

        private void AddValue(string key, object value)
        {
            _roamingContainer.Values.Add(key, JsonConvert.SerializeObject(value));
            Debug.WriteLine($"Added  : {{key: {key}, value: {JsonConvert.SerializeObject(value)}}}");
        }

        public void RemoveValue(string key)
        {
            _roamingContainer.Values.Remove(key);
            Debug.WriteLine($"Removed: {key}");
        }

        private void RewriteValue(string key, object value)
        {
            _roamingContainer.Values.Remove(key);
            _roamingContainer.Values.Add(key, JsonConvert.SerializeObject(value));
            Debug.WriteLine($"Rewrote: {{key: {key}, newValue: {JsonConvert.SerializeObject(value)}}}");
        }

        public void AddOrRewriteValue(string key, object value)
        {
            if (_roamingContainer.Values.ContainsKey(key))
                RewriteValue(key, value);
            else
                AddValue(key, value);
        }

        public void AddIfNotExist(string key, object value)
        {
            if (_roamingContainer.Values.ContainsKey(key))
                return;
            AddValue(key, value);
        }

        public T GetValue<T>(string key, object defaultValue = null)
        {
            if (_roamingContainer.Values[key] != null)
                return JsonConvert.DeserializeObject<T>(_roamingContainer.Values[key].ToString());
            return defaultValue != null ? (T) defaultValue : default(T);
        }
    }

    public static class ConfigurationKeys
    {
        public static readonly string ColumnsKey = "Columns";

        public static readonly string UsersKey = "Users";

        public static readonly string ConfigurationVersionKey = "ConfigurationVersion";
    }
}
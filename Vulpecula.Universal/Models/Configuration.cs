using System.Collections.Generic;
using System.Diagnostics;

using Windows.Storage;

using Newtonsoft.Json;

using Prism.Mvvm;

using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.Models
{
    public class Configuration : BindableBase
    {
        private static Configuration _instance;

        private readonly ApplicationDataContainer _roamingContainer;
        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        public IEnumerable<Column> Columns
            =>
            _roamingContainer.Values[ConfigurationKeys.ColumnsKey] != null
            ? JsonConvert.DeserializeObject<IEnumerable<Column>>(_roamingContainer.Values[ConfigurationKeys.ColumnsKey].ToString()) : new List<Column>();

        private Configuration()
        {
            _roamingContainer = ApplicationData.Current.RoamingSettings;
        }

        /// <summary>
        /// 設定を初期化します。
        /// </summary>
        public void Initialize() {}

        public void AddValue(string key, object value)
        {
            _roamingContainer.Values.Add(key, JsonConvert.SerializeObject(value));
            Debug.WriteLine($"Write: {JsonConvert.SerializeObject(value)}");
        }

        public void RemoveValue(string key)
        {
            _roamingContainer.Values.Remove(key);
        }

        public void RewriteValue(string key, object value)
        {
            _roamingContainer.Values.Remove(key);
            _roamingContainer.Values.Add(key, JsonConvert.SerializeObject(value));
        }

        public void AddOrRewriteValue(string key, object value)
        {
            if (_roamingContainer.Values.ContainsKey(key))
                RewriteValue(key, value);
            else
                AddValue(key, value);
        }
    }

    public static class ConfigurationKeys
    {
        public static readonly string ColumnsKey = "Columns";
    }
}
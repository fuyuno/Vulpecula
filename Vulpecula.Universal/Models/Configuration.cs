using System.Collections.Generic;
using System.Linq;

using Windows.Storage;

using Prism.Mvvm;

namespace Vulpecula.Universal.Models
{
    public class Configuration : BindableBase
    {
        private static Configuration _instance;

        private readonly ApplicationDataContainer _roamingContainer;
        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        /// <summary>
        /// 設定に保存されているカラムを取得します。
        /// </summary>
        public IEnumerable<ApplicationDataCompositeValue> Columns
        {
            get { return this._roamingContainer.Values.Where(w => w.Key.StartsWith("Column-")).Select(w => (ApplicationDataCompositeValue)w.Value).ToList(); }
        }

        private Configuration()
        {
            this._roamingContainer = ApplicationData.Current.RoamingSettings;
        }

        /// <summary>
        /// 設定を初期化します。
        /// </summary>
        public void Initialize()
        {
        }

        public void AddValues(string key, object value)
        {
            this._roamingContainer.Values.Add(key, value);
        }

        public void RemoveValue(string key)
        {
            this._roamingContainer.Values.Remove(key);
        }

        public void RewriteValue(string key, object value)
        {
            this._roamingContainer.Values.Remove(key);
            this._roamingContainer.Values.Add(key, value);
        }
    }
}
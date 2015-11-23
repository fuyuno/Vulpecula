using System.Linq;

using Foundation;

using Vulpecula.iOS.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

[assembly: Dependency(typeof (Configuration))]

namespace Vulpecula.iOS.Models
{
    public class Configuration : IConfiguration
    {
        private readonly NSUserDefaults _userDefaults;

        public Configuration()
        {
            this._userDefaults = NSUserDefaults.StandardUserDefaults;
        }

        public void SetString(string key, string value)
        {
            this._userDefaults.SetString(value, key);
        }

        public string GetString(string key)
        {
            return this._userDefaults.StringForKey(key);
        }

        public void SetInt(string key, int value)
        {
            this._userDefaults.SetInt(value, key);
        }

        public int GetInt(string key)
        {
            return (int)this._userDefaults.IntForKey(key);
        }

        public void SetDouble(string key, double value)
        {
            this._userDefaults.SetDouble(value, key);
        }

        public double GetDouble(string key)
        {
            return this._userDefaults.DoubleForKey(key);
        }

        public void SetBool(string key, bool value)
        {
            this._userDefaults.SetBool(value, key);
        }

        public bool GetBool(string key)
        {
            return this._userDefaults.BoolForKey(key);
        }

        public void SetArray(string key, string[] value)
        {
            var array = new NSMutableArray();
            foreach (var o in value)
            {
                array.Add(new NSString(o));
            }
            this._userDefaults.SetValueForKey(array, new NSString(key));
        }

        public string[] GetArray(string key)
        {
            var array = this._userDefaults.ArrayForKey(key);
            return array?.Select(o => o.ToString()).ToArray();
        }
    }
}
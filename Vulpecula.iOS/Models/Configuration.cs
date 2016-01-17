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
            _userDefaults = NSUserDefaults.StandardUserDefaults;
        }

        public void SetString(string key, string value)
        {
            _userDefaults.SetString(value, key);
            _userDefaults.Synchronize();
        }

        public string GetString(string key)
        {
            return _userDefaults.StringForKey(key);
        }

        public void SetInt(string key, int value)
        {
            _userDefaults.SetInt(value, key);
            _userDefaults.Synchronize();
        }

        public int GetInt(string key)
        {
            return (int) _userDefaults.IntForKey(key);
        }

        public void SetDouble(string key, double value)
        {
            _userDefaults.SetDouble(value, key);
            _userDefaults.Synchronize();
        }

        public double GetDouble(string key)
        {
            return _userDefaults.DoubleForKey(key);
        }

        public void SetBool(string key, bool value)
        {
            _userDefaults.SetBool(value, key);
            _userDefaults.Synchronize();
        }

        public bool GetBool(string key)
        {
            return _userDefaults.BoolForKey(key);
        }

        public void SetArray(string key, string[] value)
        {
            var array = new NSMutableArray();
            foreach (var o in value)
                array.Add(new NSString(o));
            _userDefaults.SetValueForKey(array, new NSString(key));
            _userDefaults.Synchronize();
        }

        public string[] GetArray(string key)
        {
            var array = _userDefaults.ArrayForKey(key);
            return array?.Select(o => o.ToString()).ToArray();
        }
    }
}
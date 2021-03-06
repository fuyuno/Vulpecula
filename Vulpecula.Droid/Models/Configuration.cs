using System.Linq;

using Android.Content;
using Android.Preferences;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

using Application = Android.App.Application;

[assembly: Dependency(typeof (Configuration))]

namespace Vulpecula.Droid.Models
{
    public class Configuration : IConfiguration
    {
        private readonly ISharedPreferences _preferences;

        public Configuration()
        {
            _preferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        }

        public void SetString(string key, string value)
        {
            var editor = _preferences.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public string GetString(string key)
        {
            return _preferences.GetString(key, null);
        }

        public void SetInt(string key, int value)
        {
            var editor = _preferences.Edit();
            editor.PutInt(key, value);
            editor.Commit();
        }

        public int GetInt(string key)
        {
            return _preferences.GetInt(key, 0);
        }

        public void SetDouble(string key, double value)
        {
            var editor = _preferences.Edit();
            editor.PutFloat(key, (float) value);
            editor.Commit();
        }

        public double GetDouble(string key)
        {
            return _preferences.GetFloat(key, 0);
        }

        public void SetBool(string key, bool value)
        {
            var editor = _preferences.Edit();
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        public bool GetBool(string key)
        {
            return _preferences.GetBoolean(key, false);
        }

        public void SetArray(string key, string[] value)
        {
            var editor = _preferences.Edit();
            editor.PutStringSet(key, value);
            editor.Commit();
        }

        public string[] GetArray(string key)
        {
            return _preferences.GetStringSet(key, new string[] { })?.ToArray();
        }
    }
}
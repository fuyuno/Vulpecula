using System.Linq;

using Vulpecula.Droid.Models;
using Vulpecula.Mobile.Models.Interfaces;

using Xamarin.Forms;

using Application = Android.App.Application;

[assembly: Dependency(typeof (Localization))]

namespace Vulpecula.Droid.Models
{
    public class Localization : ILocalization
    {
        /// <summary>
        /// <item>Key</item>を使って、対象ロケールの言語表示を取得します。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            var type = typeof (Resource.String);
            foreach (var fieldInfo in type.GetFields().Where(fieldInfo => fieldInfo.Name == key))
            {
                return Application.Context.GetString((int)fieldInfo.GetValue(null));
            }
            return key;
        }

        /// <summary>
        /// 完全名を使用して、対象ロケールの言語表示を取得します。
        /// </summary>
        /// <returns>The string by full name.</returns>
        /// <param name="name">Name.</param>
        public string GetStringByFullName(string name)
        {
            var type = typeof (Resource.String);
            foreach (var fieldInfo in type.GetFields().Where(fieldInfo => fieldInfo.Name == name))
            {
                return Application.Context.GetString((int)fieldInfo.GetValue(null));
            }
            return name;
        }
    }
}
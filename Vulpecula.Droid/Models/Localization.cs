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
        /// <item>Key</item>���g���āA�Ώۃ��P�[���̌���\�����擾���܂��B
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
        /// ���S�����g�p���āA�Ώۃ��P�[���̌���\�����擾���܂��B
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
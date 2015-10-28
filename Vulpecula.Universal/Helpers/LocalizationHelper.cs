using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

using Microsoft.Practices.ServiceLocation;

using Prism.Windows.AppModel;

namespace Vulpecula.Universal.Helpers
{
    public static class LocalizationHelper
    {
        private static readonly IResourceLoader Resource = ServiceLocator.Current.GetInstance<IResourceLoader>();

        public static string GetString(string key, [CallerFilePath] string path = "")
        {
            // クラス名取得のスーパー妥協
            var member = Path.GetFileNameWithoutExtension(path);
            Debug.WriteLine($"Resource requested: '{member}_{key}/Text'");
            return Resource.GetString($"{member}_{key}/Text");
        }

        public static string GetStringByFullPath(string resource)
        {
            Debug.WriteLine($"Resource requested: '{resource}/Text'");
            return Resource.GetString($"{resource}/Text");
        }
    }
}
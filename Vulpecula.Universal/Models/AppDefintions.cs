using System.Linq;

using Windows.ApplicationModel.Store;

namespace Vulpecula.Universal.Models
{
    public static class AppDefintions
    {
        public static string ConsumerKey => "b101ef32b1fd6c3e11b33f3ae4f1e91c358c02f6dd98b650e19b74edfa61d69c";

        public static string ConsumerSecret => "4bcd6e665dd41de886c90fb77a3c9430049a33693d88398ff72d6c618e62e540";

        public static string VulpeculaAppKey => "vulpecula.app.mkzk.xyz";

        /// <summary>
        /// 最大アカウント上限数
        /// デフォ4 課金+2 最大10
        /// </summary>
        public static int MaxAccounts
        {
            get
            {
                string[] keys = { "AccountPlus2-1", "AccountPlus2-2", "AccountPlus2-3" };
                return 4 + keys.Where(key => LicenseInfo.ProductLicenses[key].IsActive).Sum(key => 2);
            }
        }

        /// <summary>
        /// In-App Purchase Information
        /// </summary>
        private static LicenseInformation LicenseInfo
        {
            get
            {
#if DEBUG
                return CurrentAppSimulator.LicenseInformation;
#else
                return CurrentApp.LicenseInformation;
#endif
            }
        }
    }
}
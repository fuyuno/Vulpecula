using Windows.ApplicationModel.Store;

namespace Vulpecula.Universal.Models.Purchase
{
    /// <summary>
    /// 課金に関する情報を扱います。
    /// </summary>
    public static partial class InAppPurchase
    {
        private static readonly LicenseInformation LicenseInfo;

        static InAppPurchase()
        {
#if DEBUG
            LicenseInfo = CurrentAppSimulator.LicenseInformation;
#else
            LicenseInfo = CurrentApp.LicenseInformation;
#endif
        }
    }
}
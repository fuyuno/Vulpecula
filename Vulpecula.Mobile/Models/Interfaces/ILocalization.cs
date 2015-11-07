namespace Vulpecula.Mobile.Models.Interfaces
{
    /// <summary>
    /// OS ごとの Localization インターフェースの違いを吸収するクラスです
    /// </summary>
    public interface ILocalization
    {
        /// <summary>
        /// <item>Key</item>を使って、対象ロケールの言語表示を取得します。
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);

        /// <summary>
        /// 完全名を使用して、対象ロケールの言語表示を取得します。
        /// </summary>
        /// <returns>The string by full name.</returns>
        /// <param name="name">Name.</param>
        string GetStringByFullName(string name);
    }
}
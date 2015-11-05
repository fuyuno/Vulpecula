namespace Vulpecula.Universal.Models
{
    public enum FollowTypes
    {
        /// <summary>
        /// フォロー中
        /// </summary>
        Following,

        /// <summary>
        /// 未フォロー中
        /// </summary>
        NoFollowing,

        /// <summary>
        /// 自分
        /// </summary>
        Me,

        /// <summary>
        /// フォローリクエスト送信中
        /// </summary>
        Pending,

        /// <summary>
        /// ブロック中
        /// </summary>
        Blocking,

        /// <summary>
        /// 取得できず
        /// </summary>
        Unknown,

        /// <summary>
        /// 取得中を示します。
        /// </summary>
        Loading
    }
}
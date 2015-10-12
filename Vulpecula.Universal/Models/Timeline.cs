using Vulpecula.Models;

namespace Vulpecula.Universal.Models
{
    /// <summary>
    /// アプリケーション開始時及び終了時に、タイムラインを復元及び保存する際に使用されます。
    /// </summary>
    public class Timeline
    {
        /// <summary>
        /// タイムラインの種類
        /// </summary>
        public TimelineType Type { get; set; }

        /// <summary>
        /// 関連付けられている User
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// タイムラインの名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// プロパティ
        /// </summary>
        public object Property { get; set; }
    }
}
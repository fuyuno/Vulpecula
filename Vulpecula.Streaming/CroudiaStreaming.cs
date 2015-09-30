using System;

namespace Vulpecula.Streaming
{
    public static class CroudiaStreaming
    {
        /// <summary>
        /// 擬似 Streaming の更新間隔を取得もしくは設定します。
        /// </summary>
        public static TimeSpan TimeSpan { get; set; }

        static CroudiaStreaming()
        {
            TimeSpan = TimeSpan.FromSeconds(5);
        }
    }
}
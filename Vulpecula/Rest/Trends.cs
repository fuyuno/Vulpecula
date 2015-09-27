﻿using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    internal class Trends : CroudiaApiImpl
    {
        public Trends(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// <para>Croudiaの現在のトレンド上位10ワードを返します。</para>
        /// <para>レスポンスにはリクエストした時間、トレンドワードの名前、検索結果を表示する為のクエリが含まれます。</para>
        /// </summary>
        /// <returns></returns>
        public async Task<TrendOwner> Place(/* params Expression<Func<string, object>>[] parameters */)
        {
            return await this.Croudia.GetAsync<TrendOwner>(EndPoints.TrendsPlace, id => 1);
        }
    }
}
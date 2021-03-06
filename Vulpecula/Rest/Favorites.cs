﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Favorites : CroudiaApiImpl
    {
        internal Favorites(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// 認証ユーザー、または指定したユーザーがお気に入りした最新20件のささやきを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : int</para>
        /// </param>
        /// <returns></returns>
        [Obsolete("This method is not available because api.croudia.com disable this endpoint.", false)]
        public async Task<IEnumerable<Status>> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IEnumerable<Status>>(EndPoints.Favorites, parameters);
        }

        /// <summary>
        /// 認証ユーザー、または指定したユーザーがお気に入りした最新20件のささやきを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : int</para>
        /// </param>
        /// <returns></returns>
        [Obsolete("This method is not available because api.croudia.com disable this endpoint.", false)]
        public IEnumerable<Status> List(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await ListAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきをお気に入りにします。
        /// </summary>
        /// <param name="id">お気に入りするステータスのID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> CreateAsync(long id, params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<Status>(string.Format(EndPoints.FavoritedCreateId, id), parameters);
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきをお気に入りにします。
        /// </summary>
        /// <param name="id">お気に入りするステータスのID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public Status Create(long id, params Expression<Func<string, object>>[] parameters)
        {
            var task =
            Task.Run(async () => await CreateAsync(id, parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきのお気に入りを解除します。
        /// </summary>
        /// <param name="id">お気に入り解除するステータスのID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> DestroyAsync(long id, params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<Status>(string.Format(EndPoints.FavoritesDestroyId, id), parameters);
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきのお気に入りを解除します。
        /// </summary>
        /// <param name="id">お気に入り解除するステータスのID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public Status Destroy(long id, params Expression<Func<string, object>>[] parameters)
        {
            var task =
            Task.Run(async () => await DestroyAsync(id, parameters));
            task.Wait();
            return task.Result;
        }
    }
}
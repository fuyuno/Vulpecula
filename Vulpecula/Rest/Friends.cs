using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Friends : CroudiaApiImpl
    {
        internal Friends(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// 指定したユーザーがフォローしているユーザーのID一覧を返します。100件ずつ取得できます。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>cursor : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IDs> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IDs>(EndPoints.FriendsIds, parameters);
        }

        /// <summary>
        /// 指定したユーザーがフォローしているユーザーのID一覧を返します。100件ずつ取得できます。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>cursor : long</para>
        /// </param>
        /// <returns></returns>
        public IDs Ids(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await this.Croudia.GetAsync<IDs>(EndPoints.FriendsIds, parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 指定したユーザーのフレンド（フォローしているユーザー）オブジェクト一覧を返します。20件ずつ取得できます。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>cursor : long</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<List> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<List>(EndPoints.FriendsList, parameters);
        }

        /// <summary>
        /// 指定したユーザーのフレンド（フォローしているユーザー）オブジェクト一覧を返します。20件ずつ取得できます。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>cursor : long</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public List List(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await this.Croudia.GetAsync<List>(EndPoints.FriendsList, parameters));
            task.Wait();
            return task.Result;
        }
    }
}
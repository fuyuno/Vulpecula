using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Blocks : CroudiaApiImpl
    {
        internal Blocks(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// <para>指定したユーザーをブロックします。</para>
        /// <para>ブロックが成功した場合、指定したユーザーのオブジェクトを返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.BlocksCreate, parameters);
        }

        /// <summary>
        /// <para>指定したユーザーをブロックします。</para>
        /// <para>ブロックが成功した場合、指定したユーザーのオブジェクトを返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public User Create(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await CreateAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>指定したユーザーのブロックを解除します。</para>
        /// <para>解除が成功した場合、指定したユーザーのオブジェクトを返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.BlocksDestroy, parameters);
        }

        /// <summary>
        /// <para>指定したユーザーのブロックを解除します。</para>
        /// <para>解除が成功した場合、指定したユーザーのオブジェクトを返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public User Destroy(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await DestroyAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーでブロックしているユーザー一覧を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>cursor : long</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<List> ListAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<List>(EndPoints.BlocksList, parameters);
        }

        /// <summary>
        /// 認証ユーザーでブロックしているユーザー一覧を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>cursor : long</para>
        /// <para>trim_user : bool</para>
        /// </param>
        /// <returns></returns>
        public List List(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await ListAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーでブロックしているユーザーID一覧を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>cursor : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IDs> IdsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IDs>(EndPoints.BlocksIds, parameters);
        }

        /// <summary>
        /// 認証ユーザーでブロックしているユーザーID一覧を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>cursor : long</para>
        /// </param>
        /// <returns></returns>
        public IDs Ids(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await IdsAsync(parameters));
            task.Wait();
            return task.Result;
        }
    }
}
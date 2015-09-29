using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Mutes : CroudiaApiImpl
    {
        public MutesUsers Users => new MutesUsers(this.Croudia);

        internal Mutes(Croudia croudia) : base(croudia)
        {
        }

        public class MutesUsers : CroudiaApiImpl
        {
            internal MutesUsers(Croudia croudia) : base(croudia)
            {
            }

            /// <summary>
            /// <para>指定したユーザーをミュートします。</para>
            /// <para>ミュートが成功した場合、指定したユーザーのオブジェクトを返します。</para>
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
            /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
            /// </param>
            /// <returns></returns>
            public async Task<User> CreateAsync(params Expression<Func<string, object>>[] parameters)
            {
                return await this.Croudia.PostAsync<User>(EndPoints.MutesUsersCreate, parameters);
            }

            /// <summary>
            /// <para>指定したユーザーをミュートします。</para>
            /// <para>ミュートが成功した場合、指定したユーザーのオブジェクトを返します。</para>
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
            /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
            /// </param>
            /// <returns></returns>
            public User Create(params Expression<Func<string, object>>[] parameters)
            {
                var task =
                    Task.Run(async () => await this.Croudia.PostAsync<User>(EndPoints.MutesUsersCreate, parameters));
                task.Wait();
                return task.Result;
            }

            /// <summary>
            /// <para>指定したユーザーのミュートを解除します。</para>
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
                return await this.Croudia.PostAsync<User>(EndPoints.MutesUsersDestroy, parameters);
            }

            /// <summary>
            /// <para>指定したユーザーのミュートを解除します。</para>
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
                var task =
                    Task.Run(async () => await this.Croudia.PostAsync<User>(EndPoints.MutesUsersDestroy, parameters));
                task.Wait();
                return task.Result;
            }

            /// <summary>
            /// 認証ユーザーでミュートしているユーザー一覧を返します。
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>cursor : long</para>
            /// <para>trim_user : bool</para>
            /// </param>
            /// <returns></returns>
            public async Task<List> ListAsync(params Expression<Func<string, object>>[] parameters)
            {
                return await this.Croudia.GetAsync<List>(EndPoints.MutesUsersList, parameters);
            }

            /// <summary>
            /// 認証ユーザーでミュートしているユーザー一覧を返します。
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>cursor : long</para>
            /// <para>trim_user : bool</para>
            /// </param>
            /// <returns></returns>
            public List List(params Expression<Func<string, object>>[] parameters)
            {
                var task = Task.Run(async () => await this.Croudia.GetAsync<List>(EndPoints.MutesUsersList, parameters));
                task.Wait();
                return task.Result;
            }

            /// <summary>
            /// 認証ユーザーでミュートしているユーザーID一覧を返します。
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>cursor : long</para>
            /// </param>
            /// <returns></returns>
            public async Task<IDs> IdsAsync(params Expression<Func<string, object>>[] parameters)
            {
                return await this.Croudia.GetAsync<IDs>(EndPoints.MutesUsersIds, parameters);
            }

            /// <summary>
            /// 認証ユーザーでミュートしているユーザーID一覧を返します。
            /// </summary>
            /// <param name="parameters">
            /// <para>利用可能なパラメータ</para>
            /// <para>cursor : long</para>
            /// </param>
            /// <returns></returns>
            public IDs Ids(params Expression<Func<string, object>>[] parameters)
            {
                var task = Task.Run(async () => await this.Croudia.GetAsync<IDs>(EndPoints.MutesUsersIds, parameters));
                task.Wait();
                return task.Result;
            }
        }
    }
}
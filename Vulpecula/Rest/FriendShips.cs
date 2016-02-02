using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class FriendShips : CroudiaApiImpl
    {
        internal FriendShips(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// <para>認証ユーザーで指定したユーザーをフォローします。</para>
        /// <para>フォローに成功すると対象の <see cref="Vulpecula.Models.User"/> を返し、すでにフォローしている場合はステータスコード403が返ります。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> CreateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.FriendShipsCreate, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーで指定したユーザーをフォローします。</para>
        /// <para>フォローに成功すると対象の <see cref="Vulpecula.Models.User"/> を返し、すでにフォローしている場合はステータスコード403が返ります。</para>
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
        /// <para>認証ユーザーで指定したユーザーのフォローを解除します。</para>
        /// <para>フォロー解除に成功すると対象の <see cref="Vulpecula.Models.User"/> を返し、失敗した場合はその理由を文字列で返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> DestroyAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.FriendShipsDestroy, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーで指定したユーザーのフォローを解除します。</para>
        /// <para>フォロー解除に成功すると対象の <see cref="Vulpecula.Models.User"/> を返し、失敗した場合はその理由を文字列で返します。</para>
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
        /// 指定した2人のユーザーの詳細な関係を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>source_id : long (* Either a source_id or source_screen_name is required)</para>
        /// <para>source_screen_name : string (* Either a source_id or source_screen_name is required)</para>
        /// <para>target_id : long (* Either a target_id or target_screen_name is required)</para>
        /// <para>target_screen_name : string (* Either a target_id or target_screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<RelationShipOwner> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<RelationShipOwner>(EndPoints.FriendShipsShow, parameters);
        }

        /// <summary>
        /// 指定した2人のユーザーの詳細な関係を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>source_id : long (* Either a source_id or source_screen_name is required)</para>
        /// <para>source_screen_name : string (* Either a source_id or source_screen_name is required)</para>
        /// <para>target_id : long (* Either a target_id or target_screen_name is required)</para>
        /// <para>target_screen_name : string (* Either a target_id or target_screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public RelationShipOwner Show(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await ShowAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>認証ユーザーと指定したユーザー間のフォロー関係一覧を返します。</para>
        /// <para>最大100件まで指定可能です。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<RelationShipOwner2>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IEnumerable<RelationShipOwner2>>(EndPoints.FriendShipsLookup, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーと指定したユーザー間のフォロー関係一覧を返します。</para>
        /// <para>最大100件まで指定可能です。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public IEnumerable<RelationShipOwner2> Lookup(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await LookupAsync(parameters));
            task.Wait();
            return task.Result;
        }
    }
}
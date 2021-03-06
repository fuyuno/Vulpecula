﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Users : CroudiaApiImpl
    {
        internal Users(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// 指定したユーザーの情報を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> ShowAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<User>(EndPoints.UsersShow, parameters);
        }

        /// <summary>
        /// 指定したユーザーの情報を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public User Show(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await ShowAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>一度に最大100件のユーザーの情報を返します。</para>
        /// <para><see cref="Vulpecula.Models.User"/> には指定した各ユーザーの最新のステータスが含まれます。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<User>> LookupAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IEnumerable<User>>(EndPoints.UsersLookup, parameters);
        }

        /// <summary>
        /// <para>一度に最大100件のユーザーの情報を返します。</para>
        /// <para><see cref="Vulpecula.Models.User"/> には指定した各ユーザーの最新のステータスが含まれます。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public IEnumerable<User> Lookup(params Expression<Func<string, object>>[] parameters)
        {
            var task =
            Task.Run(async () => await LookupAsync(parameters));
            task.Wait();
            return task.Result;
        }
    }
}
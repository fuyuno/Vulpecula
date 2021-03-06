﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Account : CroudiaApiImpl
    {
        internal Account(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// 認証に成功するとリクエストしたユーザーの <see cref="Vulpecula.Models.User"/> を返します。
        /// </summary>
        /// <returns></returns>
        public async Task<User> VerifyCredentialsAsync()
        {
            return await Croudia.GetAsync<User>(EndPoints.AccountVeriryCredentials);
        }

        /// <summary>
        /// 認証に成功するとリクエストしたユーザーの <see cref="Vulpecula.Models.User"/> を返します。
        /// </summary>
        /// <returns></returns>
        public User VerifyCredentials()
        {
            var task = Task.Run(async () => await VerifyCredentialsAsync());
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>認証ユーザーのプロフィール画像を更新します。</para>
        /// <para>フォーマットはJPG, GIF, PNGに対応しており、幅が120ピクセルを超える場合は自動的に縮小されます。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateProfileImageAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.AccountUpdateProfileImage, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーのプロフィール画像を更新します。</para>
        /// <para>フォーマットはJPG, GIF, PNGに対応しており、幅が120ピクセルを超える場合は自動的に縮小されます。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public User UpdateProfileImage(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await UpdateProfileImageAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>認証ユーザーのカバー画像を更新します。フォーマットはJPG, GIF, PNGに対応しています。</para>
        /// <para>カバー画像は550×200のサイズに拡大縮小される為、550×200に近いサイズの画像をアップロードすることを利用者にオススメしてください。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateCoverImageAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.AccountUpdateCoverImage, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーのカバー画像を更新します。フォーマットはJPG, GIF, PNGに対応しています。</para>
        /// <para>カバー画像は550×200のサイズに拡大縮小される為、550×200に近いサイズの画像をアップロードすることを利用者にオススメしてください。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>image : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public User UpdateCoverImage(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await UpdateCoverImageAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーのプロフィール情報を更新します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>name : string</para>
        /// <para>url : string</para>
        /// <para>location : string</para>
        /// <para>description : string</para>
        /// <para>timersec : int</para>
        /// <para>protected : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<User> UpdateProfileAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<User>(EndPoints.AccountUpdateProfile, parameters);
        }

        /// <summary>
        /// 認証ユーザーのプロフィール情報を更新します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>name : string</para>
        /// <para>url : string</para>
        /// <para>location : string</para>
        /// <para>description : string</para>
        /// <para>timersec : int</para>
        /// <para>protected : bool</para>
        /// </param>
        /// <returns></returns>
        public User UpdateProfile(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await UpdateProfileAsync(parameters));
            task.Wait();
            return task.Result;
        }
    }
}
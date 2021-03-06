﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class SecretMails : CroudiaApiImpl
    {
        internal SecretMails(Croudia croudia) : base(croudia) {}

        /// <summary>
        /// 認証ユーザー宛の最新20件のシークレットメールを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<SecretMail>> ReceivedAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IEnumerable<SecretMail>>(EndPoints.SecretMails, parameters);
        }

        /// <summary>
        /// 認証ユーザー宛の最新20件のシークレットメールを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public IEnumerable<SecretMail> Received(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await ReceivedAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 認証ユーザーが送信した最新20件のシークレットメールを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<SecretMail>> SentAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.GetAsync<IEnumerable<SecretMail>>(EndPoints.SecretMailsSent, parameters);
        }

        /// <summary>
        /// 認証ユーザーが送信した最新20件のシークレットメールを返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public IEnumerable<SecretMail> Sent(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await SentAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>認証ユーザーから指定したユーザーへ新規にシークレットメールを送ります。送信に成功すると送信した <see cref="Vulpecula.Models.SecretMail"/> を返します。</para>
        /// <para>シークレットメールを送ることができるのはフォロワーのみで、それ以外のユーザーには送信できません。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>text : string (* Required)</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<SecretMail> NewAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<SecretMail>(EndPoints.SecretMailsNew, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーから指定したユーザーへ新規にシークレットメールを送ります。送信に成功すると送信した <see cref="Vulpecula.Models.SecretMail"/> を返します。</para>
        /// <para>シークレットメールを送ることができるのはフォロワーのみで、それ以外のユーザーには送信できません。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>text : string (* Required)</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// </param>
        /// <returns></returns>
        public SecretMail New(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await NewAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>認証ユーザーから指定したユーザーへ新規に画像付きのシークレットメールを送ります。送信に成功すると送信した <see cref="Vulpecula.Models.SecretMail"/> を返します。</para>
        /// <para>シークレットメールを送ることができるのはフォロワーのみで、それ以外のユーザーには送信できません。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>text : string (* Required)</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>media : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public async Task<SecretMail> NewWithMediaAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await Croudia.PostAsync<SecretMail>(EndPoints.SecretMailsNewWithMedia, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーから指定したユーザーへ新規に画像付きのシークレットメールを送ります。送信に成功すると送信した <see cref="Vulpecula.Models.SecretMail"/> を返します。</para>
        /// <para>シークレットメールを送ることができるのはフォロワーのみで、それ以外のユーザーには送信できません。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>text : string (* Required)</para>
        /// <para>screen_name : string (* Either a user_id or screen_name is required)</para>
        /// <para>user_id : long (* Either a user_id or screen_name is required)</para>
        /// <para>media : <see cref="System.IO.Stream"/> (* Required)</para>
        /// </param>
        /// <returns></returns>
        public SecretMail NewWithMedia(params Expression<Func<string, object>>[] parameters)
        {
            var task = Task.Run(async () => await NewWithMediaAsync(parameters));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <para>指定したIDのシークレットメールを削除します。</para>
        /// <para>削除できるのはメッセージの受信者または送信者のみになります。</para>
        /// </summary>
        /// <param name="id">削除するシークレットメールのID</param>
        /// <returns></returns>
        public async Task<SecretMail> DestroyAsync(long id)
        {
            return await Croudia.PostAsync<SecretMail>(string.Format(EndPoints.SecretMailsDestroyId, id));
        }

        /// <summary>
        /// <para>指定したIDのシークレットメールを削除します。</para>
        /// <para>削除できるのはメッセージの受信者または送信者のみになります。</para>
        /// </summary>
        /// <param name="id">削除するシークレットメールのID</param>
        /// <returns></returns>
        public SecretMail Destroy(long id)
        {
            var task = Task.Run(async () => await DestroyAsync(id));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// 指定したIDのシークレットメールを返します。
        /// </summary>
        /// <param name="id">取得するシークレットメールのID</param>
        /// <returns></returns>
        public async Task<SecretMail> ShowAsync(long id)
        {
            return await Croudia.PostAsync<SecretMail>(string.Format(EndPoints.SecretMailsShowId, id));
        }

        /// <summary>
        /// 指定したIDのシークレットメールを返します。
        /// </summary>
        /// <param name="id">取得するシークレットメールのID</param>
        /// <returns></returns>
        public SecretMail Show(long id)
        {
            var task = Task.Run(async () => await ShowAsync(id));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// シークレットメールに添付された画像を返します。
        /// </summary>
        /// <param name="id">取得する画像ID</param>
        /// <returns></returns>
#pragma warning disable 1998

        public async Task<Stream> GetSecretPhotoAsync(long id)
#pragma warning restore 1998
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// シークレットメールに添付された画像を返します。
        /// </summary>
        /// <param name="id">取得する画像ID</param>
        /// <returns></returns>
        public Stream GetSecretPhoto(long id)
        {
            throw new NotImplementedException();
        }
    }
}
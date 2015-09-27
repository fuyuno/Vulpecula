using System;
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
        internal SecretMails(Croudia croudia) : base(croudia)
        {
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
        public async Task<IEnumerable<SecretMails>> Received(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<SecretMails>>(EndPoints.SecretMails, parameters);
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
        public async Task<IEnumerable<SecretMails>> Sent(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<SecretMails>>(EndPoints.SecretMailsSent, parameters);
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
        public async Task<SecretMail> New(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<SecretMail>(EndPoints.SecretMailsNew, parameters);
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
        public async Task<SecretMail> NewWithMedia(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<SecretMail>(EndPoints.SecretMailsNewWithMedia, parameters);
        }

        /// <summary>
        /// <para>指定したIDのシークレットメールを削除します。</para>
        /// <para>削除できるのはメッセージの受信者または送信者のみになります。</para>
        /// </summary>
        /// <param name="id">削除するシークレットメールのID</param>
        /// <returns></returns>
        public async Task<SecretMail> Destroy(long id)
        {
            return await this.Croudia.PostAsync<SecretMail>(string.Format(EndPoints.SecretMailsDestroyId, id));
        }

        /// <summary>
        /// 指定したIDのシークレットメールを返します。
        /// </summary>
        /// <param name="id">取得するシークレットメールのID</param>
        /// <returns></returns>
        public async Task<SecretMail> Show(long id)
        {
            return await this.Croudia.PostAsync<SecretMail>(string.Format(EndPoints.SecretMailsShowId, id));
        }

        /// <summary>
        /// シークレットメールに添付された画像を返します。
        /// </summary>
        /// <param name="id">取得する画像ID</param>
        /// <returns></returns>
        public /* async */ Task<Stream> GetSecretPhoto(long id)
        {
            throw new NotImplementedException();
        }
    }
}
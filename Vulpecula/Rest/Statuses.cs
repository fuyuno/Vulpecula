using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest.Internal;

namespace Vulpecula.Rest
{
    public class Statuses : CroudiaApiImpl
    {
        internal Statuses(Croudia croudia) : base(croudia)
        {
        }

        /// <summary>
        /// <para>世界中のささやきの内、非公開ユーザーを除く最新20件のささやきを返します。</para>
        /// <para>* 非公開ユーザーにフォローリクエスト承認されている場合は、その非公開ユーザーのささやきも返します。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetPublicTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<Status>>(EndPoints.StatusesPublicTimeline, parameters);
        }

        /// <summary>
        /// 認証ユーザーとフォローしているユーザーのささやき最新20件を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetHomeTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<Status>>(EndPoints.StatusesHomeTimeline, parameters);
        }

        /// <summary>
        /// <para>認証ユーザー、または指定したユーザーのささやき最新20件を返します。</para>
        /// <para>screen_nameまたはuser_idパラメータを指定することで、他のユーザーのタイムラインを取得することができます。</para>
        /// <para>* ただし、非公開ユーザーのタイムラインを取得するには事前にフォローリクエストを承認している必要があります。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>screen_name : string</para>
        /// <para>user_id : long</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetUserTimelineAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<Status>>(EndPoints.StatusesUserTimeline, parameters);
        }

        /// <summary>
        /// 認証ユーザー宛の関連ささやき（＠ユーザー名を含むささやき）最新20件を返します。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<Status>> GetMentionsAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.GetAsync<IEnumerable<Status>>(EndPoints.StatusesMentions, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーでささやきます。</para>
        /// <para>in_reply_to_status_idを指定する場合、テキストに宛先ユーザーの「＠スクリーン名」を含める必要があります。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>status : string (* Required)</para>
        /// <para>in_reply_to_status_id : long</para>
        /// <para>timer : bool</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> UpdateAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(EndPoints.StatusesUpdate, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーで画像付きの投稿を行います。</para>
        /// <para>in_reply_to_status_idを指定する場合、テキストに宛先ユーザーの「＠スクリーン名」を含める必要があります。</para>
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>status : string (* Required)</para>
        /// <para>media : <see cref="System.IO.Stream"/> (* Required)</para>
        /// <para>in_reply_to_status_id : long</para>
        /// <para>timer : bool</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> UpdateWithMediaAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(EndPoints.StatusesUpdateWithMedia, parameters);
        }

        /// <summary>
        /// <para>認証ユーザーのささやきを削除します。</para>
        /// <para>* シェアの解除およびシェア＆投稿の削除もこのAPIを使用します。</para>
        /// </summary>
        /// <param name="id">削除するステータスID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> DestroyAsync(long id, params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(string.Format(EndPoints.StatusesDestroyId, id), parameters);
        }

        /// <summary>
        /// 指定したIDの <see cref="Vulpecula.Models.Status"/> を返します。
        /// </summary>
        /// <param name="id">取得するステータスID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> ShowAsync(long id, params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(string.Format(EndPoints.StatusesShowId, id), parameters);
        }

        /// <summary>
        /// <para>認証ユーザーで指定したささやきをシェアします。</para>
        /// <para>自分のささやきに対するシェアや重複するシェアはできません。</para>
        /// </summary>
        /// <param name="id">シェアするステータスのID</param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> SpreadAsync(long id, params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(string.Format(EndPoints.StatusesSpreadId, id), parameters);
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきにコメントをします。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>id : long (* Required)</para>
        /// <para>status : string (* Required)</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> CommentAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(EndPoints.StatusesComment, parameters);
        }

        /// <summary>
        /// 認証ユーザーで指定したささやきに画像を付けてコメントします。
        /// </summary>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>id : long (* Required)</para>
        /// <para>status : string (* Required)</para>
        /// <para>media : <see cref="System.IO.Stream"/> (* Required)</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// </param>
        /// <returns></returns>
        public async Task<Status> CommentWithMediaAsync(params Expression<Func<string, object>>[] parameters)
        {
            return await this.Croudia.PostAsync<Status>(EndPoints.StatusesCommentWithMedia, parameters);
        }
    }
}
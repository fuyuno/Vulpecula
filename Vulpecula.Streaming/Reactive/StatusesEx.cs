using System;
using System.Linq.Expressions;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Streaming.Reactive.Internal;

namespace Vulpecula.Streaming.Reactive
{
    public static class StatusesEx
    {
        /// <summary>
        /// <para>世界中のささやきの内、非公開ユーザーを除く最新のささやきを返します。</para>
        /// <para>* 非公開ユーザーにフォローリクエスト承認されている場合は、その非公開ユーザーのささやきも返します。</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public static IObservable<Status> GetPublicTimelineAsObservable(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new StatusesObservable(obj, StreamTypes.Statuses.Public, parameters);
        }

        /// <summary>
        /// 認証ユーザーとフォローしているユーザーのささやき最新20件を返します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public static IObservable<Status> GetHomeTimelineAsObservable(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new StatusesObservable(obj, StreamTypes.Statuses.Home, parameters);
        }

        /// <summary>
        /// <para>認証ユーザー、または指定したユーザーのささやき最新20件を返します。</para>
        /// <para>screen_nameまたはuser_idパラメータを指定することで、他のユーザーのタイムラインを取得することができます。</para>
        /// <para>* ただし、非公開ユーザーのタイムラインを取得するには事前にフォローリクエストを承認している必要があります。</para>
        /// </summary>
        /// <param name="obj"></param>
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
        public static IObservable<Status> GetUserTimelineAsObservable(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new StatusesObservable(obj, StreamTypes.Statuses.User, parameters);
        }

        /// <summary>
        /// 認証ユーザー宛の関連ささやき（＠ユーザー名を含むささやき）最新20件を返します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>include_entities : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public static IObservable<Status> GetMentionsAsObservable(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new StatusesObservable(obj, StreamTypes.Statuses.Mentions, parameters);
        }
    }
}
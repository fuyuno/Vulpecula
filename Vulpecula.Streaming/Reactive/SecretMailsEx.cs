using System;
using System.Linq.Expressions;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Streaming.Reactive.Internal;

namespace Vulpecula.Streaming.Reactive
{
    public static class SecretMailsEx
    {
        /// <summary>
        /// 認証ユーザー宛の最新20件のシークレットメールを返します。
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="parameters">
        /// <para>利用可能なパラメータ</para>
        /// <para>trim_user : bool</para>
        /// <para>since_id : long</para>
        /// <para>max_id : long</para>
        /// <para>count : long</para>
        /// </param>
        /// <returns></returns>
        public static IObservable<SecretMail> ReceivedAsObservable(this SecretMails obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new SecretMailsRxObservable(obj, StreamTypes.SecretMails.Received, parameters);
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
        public static IObservable<SecretMail> SentAsObservable(this SecretMails obj,
            params Expression<Func<string, object>>[] parameters)
        {
            return new SecretMailsRxObservable(obj, StreamTypes.SecretMails.Sent, parameters);
        }
    }
}
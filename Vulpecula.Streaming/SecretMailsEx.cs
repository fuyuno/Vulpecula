using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Streaming.Internal;

// ReSharper disable FunctionNeverReturns
// ReSharper disable InconsistentNaming

namespace Vulpecula.Streaming
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
        public static IEnumerable<SecretMail> ReceivedAsStreaming(this SecretMails obj, params Expression<Func<string, object>>[] parameters)
        {
            return ReceivedAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<SecretMail> ReceivedAsStreaming(this SecretMails obj, bool enableDummy,
                                                                    params Expression<Func<string, object>>[] parameters)
        {
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    parameters = CroudiaStreaming.AdjustParameters(parameters, since_id => id);
                }
                IEnumerable<SecretMail> secretMails;
                try
                {
                    secretMails = obj.Received(parameters).ToArray();
                }
                catch (Exception)
                {
                    secretMails = new List<SecretMail>();
                }

                if (!secretMails.Any())
                {
                    if (enableDummy)
                        yield return new DummySecretMail();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = secretMails.Max(w => w.Id);
                secretMails = secretMails.OrderBy(w => w.Id).ToArray();

                foreach (var status in secretMails)
                    yield return status;

                CroudiaStreaming.Wait();
            }
        }

        /// <summary>
        /// 認証ユーザーが送信した最新20件のシークレットメールを返します。
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
        public static IEnumerable<SecretMail> SentAsStreaming(this SecretMails obj, params Expression<Func<string, object>>[] parameters)
        {
            return SentAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<SecretMail> SentAsStreaming(this SecretMails obj, bool enableDummy,
                                                                params Expression<Func<string, object>>[] parameters)
        {
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    parameters = CroudiaStreaming.AdjustParameters(parameters, since_id => id);
                }
                IEnumerable<SecretMail> secretMails;
                try
                {
                    // API throw "404 Not Found"
                    secretMails = obj.Sent(parameters).ToArray();
                }
                catch (Exception)
                {
                    secretMails = new List<SecretMail>();
                }
                if (!secretMails.Any())
                {
                    if (enableDummy)
                        yield return new DummySecretMail();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = secretMails.Max(w => w.Id);
                secretMails = secretMails.OrderBy(w => w.Id).ToArray();

                foreach (var status in secretMails)
                    yield return status;

                CroudiaStreaming.Wait();
            }
        }
    }
}
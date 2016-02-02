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
        public static IEnumerable<Status> GetPublicTimelineAsStreaming(this Statuses obj,
                                                                       params Expression<Func<string, object>>[] parameters)
        {
            return GetPublicTimelineAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<Status> GetPublicTimelineAsStreaming(this Statuses obj, bool enableDummy,
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
                IEnumerable<Status> statuses;

                try
                {
                    statuses = obj.GetPublieTimeline(parameters).ToArray();
                }
                catch (Exception)
                {
                    statuses = new List<Status>();
                }
                if (!statuses.Any())
                {
                    if (enableDummy)
                        yield return new DummyStatus();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                CroudiaStreaming.Wait();
            }
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
        public static IEnumerable<Status> GetHomeTimelineAsStreaming(this Statuses obj,
                                                                     params Expression<Func<string, object>>[] parameters)
        {
            return GetHomeTimelineAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<Status> GetHomeTimelineAsStreaming(this Statuses obj, bool enableDummy,
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
                IEnumerable<Status> statuses;
                try
                {
                    statuses = obj.GetHomeTimeline(parameters).ToArray();
                }
                catch (Exception)
                {
                    statuses = new List<Status>();
                }
                if (!statuses.Any())
                {
                    if (enableDummy)
                        yield return new DummyStatus();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                CroudiaStreaming.Wait();
            }
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
        public static IEnumerable<Status> GetUserTimelineAsStreaming(this Statuses obj,
                                                                     params Expression<Func<string, object>>[] parameters)
        {
            return GetUserTimelineAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<Status> GetUserTimelineAsStreaming(this Statuses obj, bool enableDummy,
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
                IEnumerable<Status> statuses;
                try
                {
                    statuses = obj.GetUserTimeline(parameters).ToArray();
                }
                catch (Exception)
                {
                    statuses = new List<Status>();
                }
                if (!statuses.Any())
                {
                    if (enableDummy)
                        yield return new DummyStatus();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                CroudiaStreaming.Wait();
            }
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
        public static IEnumerable<Status> GetMentionsAsStreaming(this Statuses obj,
                                                                 params Expression<Func<string, object>>[] parameters)
        {
            return GetMentionsAsStreaming(obj, false, parameters);
        }

        internal static IEnumerable<Status> GetMentionsAsStreaming(this Statuses obj, bool enableDummy,
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
                IEnumerable<Status> statuses;
                try
                {
                    statuses = obj.GetMentions(parameters).ToArray();
                }
                catch (Exception)
                {
                    statuses = new List<Status>();
                }
                if (!statuses.Any())
                {
                    if (enableDummy)
                        yield return new DummyStatus();
                    CroudiaStreaming.Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                CroudiaStreaming.Wait();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest;

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
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    AdjustParameters(parameters, since_id => id);
                }
                var statuses = obj.GetPublieTimeline(parameters).ToArray();
                if (!statuses.Any())
                {
                    Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                Wait();
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
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    AdjustParameters(parameters, since_id => id);
                }
                var statuses = obj.GetHomeTimeline(parameters).ToArray();
                if (!statuses.Any())
                {
                    Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                Wait();
            }
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
        public static IEnumerable<Status> GetUserTimelineAsStreaming(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    AdjustParameters(parameters, since_id => id);
                }
                var statuses = obj.GetUserTimeline(parameters).ToArray();
                if (!statuses.Any())
                {
                    Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                Wait();
            }
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
        public static IEnumerable<Status> GetMentionsAsStreaming(this Statuses obj,
            params Expression<Func<string, object>>[] parameters)
        {
            var lastStatusId = 0L;
            while (true)
            {
                if (lastStatusId != 0)
                {
                    var id = lastStatusId;
                    AdjustParameters(parameters, since_id => id);
                }
                var statuses = obj.GetMentions(parameters).ToArray();
                if (!statuses.Any())
                {
                    Wait();
                    continue;
                }

                lastStatusId = statuses.Max(w => w.Id);
                statuses = statuses.OrderBy(w => w.Id).ToArray();

                foreach (var status in statuses)
                    yield return status;

                Wait();
            }
        }

        private static void AdjustParameters(Expression<Func<string, object>>[] parameters, Expression<Func<string, object>> newParameter)
        {
            var param = parameters.FirstOrDefault(w => w.Parameters[0].Name == newParameter.Parameters[0].Name);
            if (param == null)
                // ReSharper disable once RedundantAssignment
                parameters = parameters.Concat(new[] { newParameter }).ToArray();
            else
            {
                var index =
                    parameters.Select((expr, i) => new { Expr = expr, Index = i })
                        .First(w => w.Expr.Parameters[0].Name == newParameter.Parameters[0].Name)
                        .Index;
                parameters[index] = newParameter;
            }
        }

        private static void Wait()
        {
            var task = Task.Delay(CroudiaStreaming.TimeSpan);
            task.Wait();
        }
    }
}
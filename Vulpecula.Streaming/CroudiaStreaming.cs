using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Vulpecula.Streaming
{
    public static class CroudiaStreaming
    {
        /// <summary>
        /// 擬似 Streaming の更新間隔を取得もしくは設定します。
        /// </summary>
        public static TimeSpan TimeSpan { get; set; }

        static CroudiaStreaming()
        {
            TimeSpan = TimeSpan.FromSeconds(5);
        }

        internal static Expression<Func<string, object>>[] AdjustParameters(Expression<Func<string, object>>[] parameters,
                                                                            Expression<Func<string, object>> newParameter)
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
            return parameters;
        }

        internal static void Wait()
        {
            var task = Task.Delay(TimeSpan);
            task.Wait();
        }
    }
}
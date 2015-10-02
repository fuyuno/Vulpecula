using System;
using System.Linq.Expressions;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal abstract class ObservableBase<T, TV, TU> : IObservable<TU>
    {
        protected readonly T Obj;
        protected readonly TV TimelineType;
        protected readonly Expression<Func<string, object>>[] Parameters;

        protected ObservableBase(T obj, TV timelineType, Expression<Func<string, object>>[] parameters)
        {
            this.Obj = obj;
            this.TimelineType = timelineType;
            this.Parameters = parameters;
        }

        /// <summary>
        /// オブザーバーが通知を受け取ることをプロバイダーに通知します。
        /// </summary>
        /// <returns>
        /// プロバイダーが通知の送信を完了する前に、オブザーバーが通知の受信を停止できるインターフェイスへの参照。
        /// </returns>
        /// <param name="observer">通知を受け取るオブジェクト。</param>
        public abstract IDisposable Subscribe(IObserver<TU> observer);
    }
}
using System;
using System.Linq.Expressions;

using Vulpecula.Models;
using Vulpecula.Rest;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal class StatusesObservable : IObservable<Status>
    {
        private readonly Statuses _obj;
        private readonly StreamTypes.Statuses _timelineType;
        private readonly Expression<Func<string, object>>[] _parameters;

        public StatusesObservable(Statuses obj, StreamTypes.Statuses timelineType, Expression<Func<string, object>>[] parameters)
        {
            this._obj = obj;
            this._timelineType = timelineType;
            this._parameters = parameters;
        }

        /// <summary>
        /// オブザーバーが通知を受け取ることをプロバイダーに通知します。
        /// </summary>
        /// <returns>
        /// プロバイダーが通知の送信を完了する前に、オブザーバーが通知の受信を停止できるインターフェイスへの参照。
        /// </returns>
        /// <param name="observer">通知を受け取るオブジェクト。</param>
        public IDisposable Subscribe(IObserver<Status> observer)
        {
            var connection = new StatusesConnection(this._obj, this._timelineType, this._parameters, observer);
            connection.Connection();
            return connection;
        }
    }
}
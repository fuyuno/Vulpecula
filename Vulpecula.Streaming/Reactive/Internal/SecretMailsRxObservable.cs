using System;
using System.Linq.Expressions;

using Vulpecula.Models;
using Vulpecula.Rest;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal class SecretMailsRxObservable : ObservableBase<SecretMails, StreamTypes.SecretMails, SecretMail>
    {
        public SecretMailsRxObservable(SecretMails obj, StreamTypes.SecretMails timelineType,
            Expression<Func<string, object>>[] parameters) : base(obj, timelineType, parameters)
        {
        }

        /// <summary>
        /// オブザーバーが通知を受け取ることをプロバイダーに通知します。
        /// </summary>
        /// <returns>
        /// プロバイダーが通知の送信を完了する前に、オブザーバーが通知の受信を停止できるインターフェイスへの参照。
        /// </returns>
        /// <param name="observer">通知を受け取るオブジェクト。</param>
        public override IDisposable Subscribe(IObserver<SecretMail> observer)
        {
            var connection = new SecretMailsRxConnection(this.Obj, this.TimelineType, this.Parameters, observer);
            connection.Connection();
            return connection;
        }
    }
}
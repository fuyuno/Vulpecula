using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal class StatusesConnection : IDisposable
    {
        private readonly Statuses _obj;
        private readonly StreamTypes.Statuses _type;
        private readonly Expression<Func<string, object>>[] _parameters;
        private IObserver<Status> _observer;
        private CancellationTokenSource _tokenSource;

        internal StatusesConnection(Statuses obj, StreamTypes.Statuses timelineType, Expression<Func<string, object>>[] parameters, IObserver<Status> observer)
        {
            this._type = timelineType;
            this._obj = obj;
            this._parameters = parameters;
            this._observer = observer;
            this._tokenSource = new CancellationTokenSource();
        }

        internal void Connection()
        {
            try
            {
                var token = this._tokenSource.Token;
                switch (_type)
                {
                    case StreamTypes.Statuses.Public:
                        Task.Run(() =>
                        {
                            foreach (
                                var status in this._obj.GetPublicTimelineAsStreaming(this._parameters)
                                        .TakeWhile(w => !token.IsCancellationRequested))
                                this._observer.OnNext(status);
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.Home:
                        Task.Run(() =>
                        {
                            foreach (
                                var status in this._obj.GetHomeTimelineAsStreaming(this._parameters)
                                        .TakeWhile(w => !token.IsCancellationRequested))
                                this._observer.OnNext(status);
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.User:
                        Task.Run(() =>
                        {
                            foreach (
                                var status in this._obj.GetUserTimelineAsStreaming(this._parameters)
                                        .TakeWhile(w => !token.IsCancellationRequested))
                                this._observer.OnNext(status);
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.Mentions:
                        Task.Run(() =>
                        {
                            foreach (
                                var status in this._obj.GetMentionsAsStreaming(this._parameters)
                                        .TakeWhile(w => !token.IsCancellationRequested))
                                this._observer.OnNext(status);
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                this._observer.OnError(e);
            }
        }

        /// <summary>
        /// アンマネージ リソースの解放およびリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public void Dispose()
        {
            this._tokenSource.Cancel();
            this._observer = null;
        }

        private void DisposeToken()
        {
            this._tokenSource.Dispose();
            this._tokenSource = null;
            this._observer.OnCompleted();
        }
    }
}
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Streaming.Internal;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal class StatusesRxConnection : ConnectionBase<Statuses, StreamTypes.Statuses, Status>
    {
        public StatusesRxConnection(Statuses obj, StreamTypes.Statuses type, Expression<Func<string, object>>[] parameters,
            IObserver<Status> observer) : base(obj, type, parameters, observer)
        {
        }

        internal override void Connection()
        {
            try
            {
                var token = this.TokenSource.Token;
                switch (Type)
                {
                    case StreamTypes.Statuses.Public:
                        Task.Run(() =>
                        {
                            foreach (var status in this.Obj.GetPublicTimelineAsStreaming(true, this.Parameters))
                            {
                                if (!(status is DummyStatus))
                                {
                                    this.Observer.OnNext(status);
                                }
                                if (token.IsCancellationRequested)
                                {
                                    break;
                                }
                            }
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.Home:
                        Task.Run(() =>
                        {
                            foreach (var status in this.Obj.GetHomeTimelineAsStreaming(true, this.Parameters))
                            {
                                if (!(status is DummyStatus))
                                {
                                    this.Observer.OnNext(status);
                                }
                                if (token.IsCancellationRequested)
                                {
                                    break;
                                }
                            }
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.User:
                        Task.Run(() =>
                        {
                            foreach (var status in this.Obj.GetUserTimelineAsStreaming(true, this.Parameters))
                            {
                                if (!(status is DummyStatus))
                                {
                                    this.Observer.OnNext(status);
                                }
                                if (token.IsCancellationRequested)
                                {
                                    break;
                                }
                            }
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.Statuses.Mentions:
                        Task.Run(() =>
                        {
                            foreach (var status in this.Obj.GetMentionsAsStreaming(true, this.Parameters))
                            {
                                if (!(status is DummyStatus))
                                {
                                    this.Observer.OnNext(status);
                                }
                                if (token.IsCancellationRequested)
                                {
                                    break;
                                }
                            }
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                this.Observer.OnError(e);
            }
        }
    }
}
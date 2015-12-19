using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Vulpecula.Models;
using Vulpecula.Rest;
using Vulpecula.Streaming.Internal;

namespace Vulpecula.Streaming.Reactive.Internal
{
    internal class SecretMailsRxConnection : ConnectionBase<SecretMails, StreamTypes.SecretMails, SecretMail>
    {
        public SecretMailsRxConnection(SecretMails obj, StreamTypes.SecretMails type,
            Expression<Func<string, object>>[] parameters, IObserver<SecretMail> observer)
            : base(obj, type, parameters, observer)
        {
        }

        internal override void Connection()
        {
            try
            {
                var token = this.TokenSource.Token;
                switch (Type)
                {
                    case StreamTypes.SecretMails.Received:
                        Task.Run(() =>
                        {
                            foreach (var mail in this.Obj.ReceivedAsStreaming(true, this.Parameters))
                            {
                                if (!(mail is DummySecretMail))
                                {
                                    this.Observer.OnNext(mail);
                                }
                                if (token.IsCancellationRequested)
                                {
                                    break;
                                }
                            }
                        }, token).ContinueWith(t => this.DisposeToken(), token);
                        break;

                    case StreamTypes.SecretMails.Sent:
                        Task.Run(() =>
                        {
                            while (!token.IsCancellationRequested)
                            {
                                foreach (var mail in this.Obj.SentAsStreaming(true, this.Parameters))
                                {
                                    if (!(mail is DummySecretMail))
                                    {
                                        this.Observer.OnNext(mail);
                                    }
                                    if (token.IsCancellationRequested)
                                    {
                                        break;
                                    }
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
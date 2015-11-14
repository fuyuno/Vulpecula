using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Vulpecula.Models;
using Vulpecula.Streaming.Reactive;
using Vulpecula.Universal.Models.Services.Primitive;

namespace Vulpecula.Universal.Models.Services
{
    public class DirectMessageTimelineService : TimelineServiceBase<SecretMail>
    {
        private IConnectableObservable<SecretMail> _connectableObservable;
        private IDisposable _disposable;

        public DirectMessageTimelineService(CroudiaProvider provider) : base(provider)
        {
        }

        public override void Suspend()
        {
            this._disposable.Dispose();
        }

        /// <summary>
        /// アンマネージ リソースの解放またはリセットに関連付けられているアプリケーション定義のタスクを実行します。
        /// </summary>
        public override void Dispose()
        {
            //
        }

        public override void Start()
        {
            var observable = this.Provider.Croudia.SecretMails.SentAsObservable();
            observable.Merge(this.Provider.Croudia.SecretMails.ReceivedAsObservable());

            _connectableObservable = observable.Publish();
            foreach (var action in this.Subscribers)
                _connectableObservable.Subscribe(w => action.Invoke(w));
            this._disposable = _connectableObservable.Connect();
            StartSubscriberRequest();
        }

        protected override void SubscriberAdded(Action<SecretMail> obj)
        {
            if (obj != null)
            {
                _connectableObservable.Subscribe(obj);
            }
        }
    }
}
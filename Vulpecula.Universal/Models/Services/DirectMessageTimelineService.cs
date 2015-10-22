using System;
using System.Reactive.Linq;

using Vulpecula.Models;
using Vulpecula.Streaming.Reactive;

namespace Vulpecula.Universal.Models.Services
{
    public class DirectMessageTimelineService : TimelineServiceBase<SecretMail>
    {
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

            var observer = observable.Publish();
            foreach (var action in this.Subscribers)
                observer.Subscribe(w => action.Invoke(w));
            this._disposable = observer.Connect();
        }
    }
}
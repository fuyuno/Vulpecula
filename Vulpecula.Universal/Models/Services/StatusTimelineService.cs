using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using Vulpecula.Models;
using Vulpecula.Streaming.Reactive;
using Vulpecula.Universal.Models.Services.Primitive;
using Vulpecula.Universal.Models.Timelines;

namespace Vulpecula.Universal.Models.Services
{
    public class StatusTimelineService : TimelineServiceBase<Status>
    {
        private readonly TimelineType _type;
        private IConnectableObservable<Status> _connectableObservable;
        private IDisposable _disposable;

        public StatusTimelineService(CroudiaProvider provider, TimelineType type) : base(provider)
        {
            _type = type;
        }

        public override void Suspend()
        {
            _disposable.Dispose();
        }

        public override void Dispose()
        {
            _disposable.Dispose();
        }

        public override void Start()
        {
            _connectableObservable = ConnectTimeline().Publish();
            foreach (var subscriber in Subscribers)
                _connectableObservable.Subscribe(w => subscriber.Invoke(w));
            _disposable = _connectableObservable.Connect();
            StartSubscriberRequest();
        }

        protected override void SubscriberAdded(Action<Status> obj)
        {
            if (obj != null)
                _connectableObservable.Subscribe(obj);
        }

        private IObservable<Status> ConnectTimeline()
        {
            switch (_type)
            {
                case TimelineType.Home:
                    return Provider.Croudia.Statuses.GetHomeTimelineAsObservable();

                case TimelineType.Mentions:
                    return Provider.Croudia.Statuses.GetMentionsAsObservable();

                case TimelineType.Public:
                    return Provider.Croudia.Statuses.GetPublicTimelineAsObservable();
            }
            return null;
        }
    }
}
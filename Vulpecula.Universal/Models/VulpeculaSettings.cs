using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Windows.Storage;

using Prism.Mvvm;

using Vulpecula.Models;

namespace Vulpecula.Universal.Models
{
    public class VulpeculaSettings : BindableBase
    {
        private readonly ApplicationDataContainer _roamingContainer;

        public VulpeculaSettings()
        {
            this._roamingContainer = ApplicationData.Current.RoamingSettings;
        }

        /// <summary>
        /// 設定を初期化します。
        /// </summary>
        public void Initialize()
        {
        }

        #region Timelines

        public IEnumerable<Timeline> Timelines
        {
            get
            {
                return this._roamingContainer.Values.Where(w => w.Key.StartsWith("Timeline-")).Select(w =>
                    this.ParseTimeline((ApplicationDataCompositeValue)w.Value)).Reverse().ToList();
            }
        }

        /// <summary>
        /// タイムラインを初期化します。
        /// </summary>
        /// <param name="user"></param>
        public void InitializeTimeline(User user)
        {
            this.AddTimeline(new Timeline { Type = TimelineType.Public, Name = "public", User = user });
            this.AddTimeline(new Timeline { Type = TimelineType.Mentions, Name = "mentions", User = user });
            this.AddTimeline(new Timeline { Type = TimelineType.DirectMessages, Name = "messages", User = user });
        }

        /// <summary>
        /// タイムラインを追加します。
        /// </summary>
        /// <param name="timeline"></param>
        public void AddTimeline(Timeline timeline)
        {
            var composite = new ApplicationDataCompositeValue
            {
                [nameof(Timeline.Type)] = timeline.Type.ToString(),
                [nameof(Timeline.Name)] = timeline.Name,
                [nameof(Timeline.User)] = timeline.User.Id,
                [nameof(Timeline.Property)] = timeline.Property
            };

            this._roamingContainer.Values.Add($"Timeline-{Guid.NewGuid()}", composite);
        }

        /// <summary>
        /// タイムラインを削除します。
        /// </summary>
        /// <param name="timeline"></param>
        public void RemoveTimeline(Timeline timeline)
        {
            var timelines = this._roamingContainer.Values.Where(w => w.Key.StartsWith("Timeline-")).ToList();
            foreach (var timeline1 in timelines)
            {
                if ((long)((ApplicationDataCompositeValue)timeline1.Value)["User"] != timeline.User.Id)
                    continue;
                this._roamingContainer.Values.Remove(timeline1.Key);
                break;
            }
        }

        private Timeline ParseTimeline(ApplicationDataCompositeValue composite)
        {
            TimelineType type;
            if (!Enum.TryParse(composite[nameof(Timeline.Type)].ToString(), out type))
                throw new InvalidDataException();

            var timeline = new Timeline
            {
                Type = type,
                Name = (string)composite[nameof(Timeline.Name)],
                User = new User { Id = (long)composite[nameof(Timeline.User)] },
                Property = composite[nameof(Timeline.Property)]
            };
            return timeline;
        }

        #endregion
    }
}
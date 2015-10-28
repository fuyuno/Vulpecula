using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using Windows.UI.Core;

using Vulpecula.Models;
using Vulpecula.Models.Base;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Services;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.Models.Timelines.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines.Primitives;

// ReSharper disable PossibleMultipleEnumeration

namespace Vulpecula.Universal.ViewModels.Timelines
{
    public class ColumnViewModel : ViewModel
    {
        private readonly Column _column;
        private readonly User _user;

        public string Name => this._column.Name;

        public string Icon => this._user.ProfileImageUrlHttps;
        public ObservableCollection<StatusViewModel> Statuses { get; }

        private ColumnViewModel(Column column, CroudiaProvider provider)
        {
            this._column = column;
            this._user = provider.User;
            this.Statuses = new ObservableCollection<StatusViewModel>();

            if (this._column.Type == TimelineType.DirectMessages || this._column.Type == TimelineType.DirectMessagesAll)
            {
                var service = new DirectMessageTimelineService(provider);
                service.Subscribers.Add(this.AddTimeline);
                ServiceProvider.RegisterService(service);
            }
            else
            {
                var service = new StatusTimelineService(provider, this._column.Type);
                service.Subscribers.Add(this.AddTimeline);
                ServiceProvider.RegisterService(service);
            }
        }

        public static ColumnViewModel Create(Column column)
        {
            if (AccountManager.Instance.Providers.Any(w => w.User.Id == column.UserId))
                throw new KeyNotFoundException($"UserId:{column.UserId} is not found in users that loading.");
            return new ColumnViewModel(column, AccountManager.Instance.Providers.Single(w => w.User.Id == column.UserId));
        }

        private async void AddTimeline(StatusBase status)
        {
            var vm = new StatusViewModel(new StatusModel(status));
            await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => this.Statuses.Insert(0, vm));
        }
    }
}
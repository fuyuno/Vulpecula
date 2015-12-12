using System;
using System.Collections.ObjectModel;

using System.Reactive.Linq;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Streaming.Reactive;
using Prism.Commands;
using Vulpecula.Mobile.Views.Popups;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class DirectMessageTimelineViewModel : TabbedViewModel
    {
        public ObservableCollection<DirectMailViewModel> Statuses { get; set; }

        private readonly CroudiaProvider _provider;
        private long _receivedLastId = 0;
        private long _sentLastId = 0;
        private IDisposable _disposable;

        public DirectMessageTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider)
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString("Messages");
            Icon = "message";
            NavigationTitle = this.GetLocalizedString("MessagesTimeline");
            this.Statuses = new ObservableCollection<DirectMailViewModel>();
            this._provider = provider;
        }

        public override void OnTabNavigatedFrom()
        {
            this._disposable?.Dispose();
        }

        public override void OnTabNavigatedTo()
        {
            var obs = this._provider.Croudia.SecretMails.ReceivedAsObservable(since_id => this._receivedLastId);
            obs.Merge(this._provider.Croudia.SecretMails.SentAsObservable(since_id => this._sentLastId));
            this._disposable = obs.Subscribe(w =>
            {
                this.Statuses.Insert(0, new DirectMailViewModel(this.Localization, this.NavigationService, w));
                if (w.SenderId == this._provider.User.Id)
                {
                    this._sentLastId = w.Id;
                }
                else
                {
                    this._receivedLastId = w.Id;
                }
            });
        }

        #region NavigateCommand

        private DelegateCommand _navigateCommand;

        public DelegateCommand NavigateCommand => _navigateCommand ?? (_navigateCommand = new DelegateCommand(Navigate));

        private void Navigate()
        {
            NavigationService.Navigate<StatusPage>();
        }

        #endregion
    }
}
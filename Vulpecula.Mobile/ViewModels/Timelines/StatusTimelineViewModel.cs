using System;
using System.Collections.ObjectModel;

using Prism.Navigation;

using Vulpecula.Mobile.Models;
using Vulpecula.Mobile.Models.Interfaces;
using Vulpecula.Mobile.ViewModels.Primitives;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Streaming.Reactive;

namespace Vulpecula.Mobile.ViewModels.Timelines
{
    public class StatusTimelineViewModel : TabbedViewModel
    {
        private readonly CroudiaProvider _provider;

        public ObservableCollection<StatusViewModel> Statuses { get; }

        public StatusTimelineViewModel(ILocalization localization, INavigationService navigationService, CroudiaProvider provider, string title, string icon, string navigationTitle = "")
            : base(localization, navigationService)
        {
            Title = this.GetLocalizedString(title);
            Icon = GetLocalizedString(icon);
            NavigationTitle = GetLocalizedString(string.IsNullOrWhiteSpace(navigationTitle) ? title : navigationTitle);
            this.Statuses = new ObservableCollection<StatusViewModel>();
            this._provider = provider;


            this.CompositeDisposable.Add(this._provider.Croudia.Statuses.GetPublicTimelineAsObservable().Subscribe(w => 
                this.Statuses.Insert(0, new StatusViewModel(this.Localization, w))));
        }

        /// <summary>
        /// Called when the implementer has been navigated to.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        /// <summary>
        /// Called when the implementer is being navigated away from.
        /// </summary>
        /// <param name="parameters">The navigation parameters.</param>
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }
    }
}
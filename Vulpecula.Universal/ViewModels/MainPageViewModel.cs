using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.Practices.ObjectBuilder2;

using Prism.Windows.Navigation;

using Vulpecula.Models;
using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.Services;
using Vulpecula.Universal.Services.Primitive;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel
    {
        private readonly INavigationService _navigationService;

        #region Properties

        public ObservableCollection<ColumnViewModel> Colmuns { get; private set; }

        #endregion

        public MainPageViewModel(INavigationService navigationService)
        {
            Colmuns = new ObservableCollection<ColumnViewModel>();
            _navigationService = navigationService;
            ViewModelHelper.SubscribeNotifyCollectionChanged(ColumnManager.Instance.Columns, Colmuns,
                                                             (Column w) => ColumnViewModel.Create(w, _navigationService));
        }

        #region Events

        public async void Initialize()
        {
            while (!ColumnManager.Instance.IsInitialized)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(500));
                Debug.WriteLine("Waiting for Initializing Columns.");
            }
            if (AccountManager.Instance.Users.Count == 0)
            {
                await AccountManager.Instance.AuthorizationAccount();
                if (AccountManager.Instance.Users.Count > 0)
                    ColumnManager.Instance.SetupInitialColumns(AccountManager.Instance.Users.First().Id);
            }
        }

        #endregion

        #region Overrides of ViewModelBase

        /// <summary>
        /// Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="T:Prism.Windows.Navigation.NavigatedToEventArgs" /> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            ColumnManager.Instance.Columns.ForEach(w => Colmuns.Add(ColumnViewModel.Create(w, _navigationService)));
        }

        /// <summary>
        /// This method will be called when navigating away from a page. You can use this method to save your view model data in case of a suspension event.
        /// </summary>
        /// <param name="e">The <see cref="T:Prism.Windows.Navigation.NavigatingFromEventArgs" /> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        /// <param name="suspending">if set to <c>true</c> you are navigating away from this viewmodel due to a suspension event.</param>
        public override void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            foreach (var suspendableService in ServiceProvider.SuspendableServices)
            {
                (suspendableService as TimelineServiceBase<Status>)?.Clear();
                (suspendableService as TimelineServiceBase<SecretMail>)?.Clear();
            }
        }

        #endregion
    }
}
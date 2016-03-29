using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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
        private readonly AccountManager _accountManager;
        private readonly ColumnManager _columnManager;
        private readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService, AccountManager accountManager, ColumnManager columnManager)
        {
            _navigationService = navigationService;
            _accountManager = accountManager;
            _columnManager = columnManager;
            Colmuns = new ObservableCollection<ColumnViewModel>();
            ViewModelHelper.SubscribeNotifyCollectionChanged(_columnManager.Columns, Colmuns,
                                                             (Column w) => new ColumnViewModel(w, _navigationService));
        }

        #region Properties

        public ObservableCollection<ColumnViewModel> Colmuns { get; }

        #endregion Properties

        #region Events

        public async void Initialize()
        {
            await _accountManager.InitializeAccounts();
            await _columnManager.InitializeColumns();

            if (_accountManager.Accounts.Any() && !_columnManager.Columns.Any())
                _columnManager.SetupInitialColumns(_accountManager.Accounts.First());
            if (_accountManager.Accounts.Count != 0)
                return;
            await _accountManager.AuthorizationAccount();
            if (_accountManager.Accounts.Count > 0)
                _columnManager.SetupInitialColumns(_accountManager.Accounts.First());
        }

        #endregion Events

        #region Overrides of ViewModelBase

        /// <summary>
        ///     Called when navigation is performed to a page. You can use this method to load state if it is available.
        /// </summary>
        /// <param name="e">The <see cref="T:Prism.Windows.Navigation.NavigatedToEventArgs" /> instance containing the event data.</param>
        /// <param name="viewModelState">The state of the view model.</param>
        public override void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            _columnManager.Columns.ForEach(w => Colmuns.Add(new ColumnViewModel(w, _navigationService)));
        }

        /// <summary>
        ///     This method will be called when navigating away from a page. You can use this method to save your view model data
        ///     in case of a suspension event.
        /// </summary>
        /// <param name="e">
        ///     The <see cref="T:Prism.Windows.Navigation.NavigatingFromEventArgs" /> instance containing the event
        ///     data.
        /// </param>
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

        #endregion Overrides of ViewModelBase
    }
}
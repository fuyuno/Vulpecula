using System.Collections.ObjectModel;

using JetBrains.Annotations;

using Vulpecula.Universal.Helpers;
using Vulpecula.Universal.Models;
using Vulpecula.Universal.Models.Timelines;
using Vulpecula.Universal.ViewModels.Primitives;
using Vulpecula.Universal.ViewModels.Timelines;

namespace Vulpecula.Universal.ViewModels
{
    [UsedImplicitly]
    public class MainPageViewModel : ViewModel
    {
        #region Properties

        public ObservableCollection<ColumnViewModel> Colmuns { get; }

        #endregion

        public MainPageViewModel()
        {
            this.Colmuns = new ObservableCollection<ColumnViewModel>();

            ViewModelHelper.SubscribeNotifyCollectionChanged(ColumnManager.Instance.Columns, this.Colmuns, (Column w) => ColumnViewModel.Create(w));
        }
    }
}
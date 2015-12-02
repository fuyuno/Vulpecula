using Prism.Navigation;

using Vulpecula.Mobile.Models.Interfaces;

namespace Vulpecula.Mobile.ViewModels.Primitives
{
    /// <summary>
    /// TabbedView content ViewModel.
    /// </summary>
    public class TabbedViewModel : NavigationalViewModel
    {
        #region Title

        private string _title;

        public string Title
        {
            get { return this._title; }
            set { this.SetProperty(ref this._title, value); }
        }

        #endregion

        public string Icon { get; set; }

        protected TabbedViewModel(ILocalization localization, INavigationService navigationService)
            : base(localization, navigationService)
        {
        }

        public virtual void OnTabNavigatedTo()
        {
            // Override
        }

        public virtual void OnTabNavigatedFrom()
        {
            // Override
        }
    }
}
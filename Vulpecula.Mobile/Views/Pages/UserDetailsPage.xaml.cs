using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Vulpecula.Mobile.ViewModels.Pages;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Mobile.Views.Timelines.Primitives;
using Vulpecula.Models;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Views.Pages
{
    public partial class UserDetailsPage : ContentPage
    {
        public UserDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.Statuses.Children.Count > 0)
            {
                return;
            }
            Task.Run(async () =>
                {
                    var vm = (this.BindingContext as UserDetailsPageViewModel);
                    User user;
                    if (Device.OS == TargetPlatform.Android)
                    {
                        // When android, vm.Model is null. Why?
                        do
                        {
                            user = vm.Model;
                        }
                        while (user == null);
                    }
                    else
                    {
                        user = vm.Model;
                    }
                    var statuses = await vm.AccountManager.Providers.First().Croudia.Statuses.GetUserTimelineAsync(screen_name => user.ScreenName);
                    foreach (var status in statuses)
                    {
                        var s = new StatusView()
                        {
                            BindingContext = new StatusViewModel(vm.Location, vm.NavigationService, status),
                            HorizontalOptions = LayoutOptions.StartAndExpand
                        };
                        Device.BeginInvokeOnMainThread(() => this.Statuses.Children.Add(s));
                    }
                });
        }
    }
}

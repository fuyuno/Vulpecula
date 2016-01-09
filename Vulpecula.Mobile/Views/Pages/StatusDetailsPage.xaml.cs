using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Vulpecula.Mobile.ViewModels.Pages;
using Vulpecula.Mobile.ViewModels.Timelines.Primitives;
using Vulpecula.Mobile.Views.Timelines.Primitives;
using Vulpecula.Models;

using Xamarin.Forms;


namespace Vulpecula.Mobile.Views.Pages
{
    public partial class StatusDetailsPage : ContentPage
    {
        public StatusDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (this.Conversations.Children.Count > 0)
            {
                return;
            }
            Task.Run(async () =>
                {
                    var vm = (this.BindingContext as StatusDetailsPageViewModel);
                    Status status = vm.Model;
                    while (status.InReplyToStatusId.HasValue && status.InReplyToStatusId.Value > 0)
                    {
                        try
                        {
                            status = await vm.AccountManager.Providers.First().Croudia.Statuses.ShowAsync(status.InReplyToStatusId.Value);
                            // #14
                            if (status.User == null)
                            {
                                break;
                            }
                            var svm = new StatusViewModel(vm.Localization, vm.NavigationService, status);
                            Device.BeginInvokeOnMainThread(() =>
                                this.Conversations.Children.Add(new StatusView()
                                    {
                                        BindingContext = svm,
                                        HorizontalOptions = LayoutOptions.StartAndExpand
                                    }));
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e);
                            // #14
                        }
                    }
                });
        }
    }
}


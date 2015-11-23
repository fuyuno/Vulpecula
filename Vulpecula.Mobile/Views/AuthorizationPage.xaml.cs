using Vulpecula.Mobile.ViewModels;

using Xamarin.Forms;

namespace Vulpecula.Mobile.Views
{
    public partial class AuthorizationPage : ContentPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private async void WebView_OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            var navigated = (this.BindingContext as AuthorizationPageViewModel)?.WebViewNavigated(sender, e);
            if (navigated != null)
                await navigated;
        }
    }
}
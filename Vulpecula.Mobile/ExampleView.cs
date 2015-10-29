using Xamarin.Forms;

namespace Vulpecula.Mobile
{
    public class ExampleView : Application
    {
        public ExampleView()
        {
            this.MainPage = new ContentPage
            {
                Content = new Label
                {
                    Text = "Hello Xamarin!",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    FontSize = 20
                }
            };
        }
    }
}
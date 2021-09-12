using CustomTransitions.Features;
using Xamarin.Forms;

namespace CustomTransitions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var firstPage = new FirstPage();
            NavigationPage.SetHasNavigationBar(firstPage, false);
            MainPage = new NavigationPage(firstPage);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

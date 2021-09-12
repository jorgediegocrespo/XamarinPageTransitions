using System.Threading.Tasks;
using CustomTransitions.Base;
using CustomTransitions.Features;
using Xamarin.Forms;

[assembly: Dependency(typeof(CustomTransitions.Services.NavigationService))]
namespace CustomTransitions.Services
{
    public class NavigationService : INavigationService
    {
        private Page CurrentPage => ((NavigationPage)Application.Current.MainPage).CurrentPage;
        private INavigation MainNavigation => ((NavigationPage)Application.Current.MainPage).Navigation;

        public async Task NavigateToSecondPage()
        {
            await ((IAnimatedPage)CurrentPage).RunDisappearingAnimationAsync();

            var view = new SecondPage();
            NavigationPage.SetHasNavigationBar(view, false);
            await MainNavigation.PushAsync(view, false);
        }

        public async Task NavigateBack()
        {
            await ((IAnimatedPage)CurrentPage).RunDisappearingAnimationAsync();
            await MainNavigation.PopAsync(false);
        }
    }
}

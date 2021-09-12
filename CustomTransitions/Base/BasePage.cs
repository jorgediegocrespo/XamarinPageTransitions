using System.Threading.Tasks;
using CustomTransitions.Services;
using Xamarin.Forms;

namespace CustomTransitions.Base
{
    public class BasePage : ContentPage, IAnimatedPage
    {
        protected readonly INavigationService navigationService;
        private bool appearingAnimationDone;

        public BasePage()
        {
            navigationService = DependencyService.Get<INavigationService>();
        }

        protected override async void OnAppearing()
        {
            if (Device.RuntimePlatform == Device.iOS)
                await RuniOSAppearingAnimationAsync();
            else
                await RunAndroidAppearingAnimationAsync(Width, Height);
        }

        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            await RunAndroidAppearingAnimationAsync(width, height);
        }

        private async Task RuniOSAppearingAnimationAsync()
        {
            if (!appearingAnimationDone)
                await RunAppearingAnimationAsync();
        }

        private async Task RunAndroidAppearingAnimationAsync(double width, double height)
        {
            if (!appearingAnimationDone && width > 0 && height > 0)
                await RunAppearingAnimationAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            navigationService.NavigateBack();
            return true;
        }

        public virtual Task RunAppearingAnimationAsync()
        {
            appearingAnimationDone = true;
            return Task.CompletedTask;
        }

        public virtual Task RunDisappearingAnimationAsync()
        {
            appearingAnimationDone = false;
            return Task.CompletedTask;
        }
    }
}

using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomTransitions.Base
{
    public class BasePage : ContentPage, IAnimatedPage
    {
        private bool appearingAnimationDone;

        public BasePage()
        {
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
            {
                appearingAnimationDone = true;
                await RunAppearingAnimationAsync();
            }
        }

        private async Task RunAndroidAppearingAnimationAsync(double width, double height)
        {
            if (!appearingAnimationDone && width > 0 && height > 0)
            {
                appearingAnimationDone = true;
                await RunAppearingAnimationAsync();
            }
        }

        protected override void OnDisappearing()
        {
            appearingAnimationDone = false;
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            //TODO
            return true;
        }

        public virtual Task RunAppearingAnimationAsync()
        {
            return Task.CompletedTask;
        }

        public virtual Task RunDisappearingAnimationAsync()
        {
            return Task.CompletedTask;
        }
    }
}

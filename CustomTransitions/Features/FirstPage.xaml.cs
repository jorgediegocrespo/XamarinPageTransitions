using System.Threading.Tasks;
using CustomTransitions.Base;
using Xamarin.Forms;

namespace CustomTransitions.Features
{
    public partial class FirstPage : IAnimatedPage
    {
        private const double X_TRANSLATION = 500;

        public FirstPage()
        {
            InitializeComponent();
            lbName.TranslationX = X_TRANSLATION;
            lbJob.TranslationX = X_TRANSLATION;
        }

        public override async Task RunAppearingAnimationAsync()
        {
            await base.RunAppearingAnimationAsync();
            
            var animation = new Animation();
            animation.Add(0, 0.5, new Animation(x => frImage.Opacity = x, frImage.Opacity, 1));
            animation.Add(0.2, 0.8, new Animation(x => lbName.TranslationX = x, lbName.TranslationX, 0, Easing.BounceOut));
            animation.Add(0.3, 0.9, new Animation(x => lbJob.TranslationX = x, lbJob.TranslationX, 0, Easing.BounceOut));
            animation.Add(0.4, 1, new Animation(x => btDetail.Scale = x, btDetail.Scale, 1, Easing.SpringOut));

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            animation.Commit(this, "appearingAnimation", length: 2000, finished: (x, y) =>
            {
                frImage.Opacity = 1;
                lbName.TranslationX = 0;
                lbJob.TranslationX = 0;
                btDetail.Scale = 1;

                tcs.SetResult(true);
            });

            await tcs.Task;
        }

        public override async Task RunDisappearingAnimationAsync()
        {
            await base.RunDisappearingAnimationAsync();

            var animation = new Animation();
            animation.Add(0.2, 0.8, new Animation(x => lbName.TranslationX = x, lbName.TranslationX, X_TRANSLATION, Easing.SpringOut));
            animation.Add(0.3, 0.9, new Animation(x => lbJob.TranslationX = x, lbJob.TranslationX, X_TRANSLATION, Easing.SpringOut));
            animation.Add(0.4, 1, new Animation(x => btDetail.Scale = x, btDetail.Scale, 0, Easing.SpringIn));

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            animation.Commit(this, "disappearingAnimation", length: 2000, finished: (System.Action<double, bool>)((x, y) =>
            {
                //frImage.Opacity = 0;
                lbName.TranslationX = FirstPage.X_TRANSLATION;
                lbJob.TranslationX = FirstPage.X_TRANSLATION;
                btDetail.Scale = 0;

                tcs.SetResult(true);
            }));

            await tcs.Task;
        }

        private async void btDetail_Clicked(System.Object sender, System.EventArgs e)
        {
            await navigationService.NavigateToSecondPage();
        }
    }
}

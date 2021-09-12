using System.Threading.Tasks;
using CustomTransitions.Base;
using Xamarin.Forms;

namespace CustomTransitions.Features
{
    public partial class SecondPage : IAnimatedPage
    {
        private const double Y_TRANSLATION = 1000;

        public SecondPage()
        {
            InitializeComponent();
            foreach (View child in slInfo.Children)
                child.TranslationY = Y_TRANSLATION;
        }

        public override async Task RunAppearingAnimationAsync()
        {
            await base.RunAppearingAnimationAsync();

            var animation = new Animation();
            double duration = 0.5;
            double step = (0.8 - duration) / (slInfo.Children.Count - 1);
            for (int i = 0; i < slInfo.Children.Count; i++)
            {
                View child = slInfo.Children[i];
                double beginAt = i * step;
                animation.Add(beginAt, beginAt + duration, new Animation(x => child.TranslationY = x, child.TranslationY, 0));
            }
            double columnWith = (gridContent.Width - 100) / 2;
            animation.Add(0, 0.5, new Animation(x => gridContent.ColumnDefinitions[0].Width = new GridLength(x, GridUnitType.Absolute), gridContent.ColumnDefinitions[0].Width.Value, columnWith));
            animation.Add(0.8, 1, new Animation(x => btBack.Scale = x, btBack.Scale, 1, Easing.SpringOut));

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            animation.Commit(this, "appearingAnimation", length: 2000, finished: (x, y) =>
            {
                foreach (View child in slInfo.Children)
                    child.TranslationY = 0;

                btBack.Scale = 1;
                gridContent.ColumnDefinitions[0].Width = new GridLength(columnWith, GridUnitType.Absolute);

                tcs.SetResult(true);
            });

            await tcs.Task;
        }

        public override async Task RunDisappearingAnimationAsync()
        {
            var animation = new Animation();
            double duration = 0.5;
            double step = (0.8 - duration) / (slInfo.Children.Count - 1);
            for (int i = 0; i < slInfo.Children.Count; i++)
            {
                View child = slInfo.Children[slInfo.Children.Count - 1 - i];
                double beginAt = 0.2 + (i * step);
                animation.Add(beginAt, beginAt + duration, new Animation(x => child.TranslationY = x, child.TranslationY, Y_TRANSLATION));
            }
            animation.Add(0, 0.2, new Animation(x => btBack.Scale = x, btBack.Scale, 0, Easing.SpringOut));
            animation.Add(0.5, 1, new Animation(x => gridContent.ColumnDefinitions[0].Width = new GridLength(x, GridUnitType.Absolute), gridContent.ColumnDefinitions[0].Width.Value, 0));

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();
            animation.Commit(this, "appearingAnimation", length: 2000, finished: (x, y) =>
            {
                foreach (View child in slInfo.Children)
                    child.TranslationY = Y_TRANSLATION;

                btBack.Scale = 0;
                gridContent.ColumnDefinitions[0].Width = new GridLength(0, GridUnitType.Absolute);

                tcs.SetResult(true);
            });

            await tcs.Task;
        }

        private async void btBack_Clicked(System.Object sender, System.EventArgs e)
        {
            await navigationService.NavigateBack();
        }
    }
}

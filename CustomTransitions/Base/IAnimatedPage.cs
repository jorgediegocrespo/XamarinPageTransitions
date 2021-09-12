using System.Threading.Tasks;

namespace CustomTransitions.Base
{
    public interface IAnimatedPage
    {
        Task RunDisappearingAnimationAsync();
        Task RunAppearingAnimationAsync();
    }
}

using System.Threading.Tasks;

namespace CustomTransitions.Services
{
    public interface INavigationService
    {
        Task NavigateToSecondPage();
        Task NavigateBack();
    }
}

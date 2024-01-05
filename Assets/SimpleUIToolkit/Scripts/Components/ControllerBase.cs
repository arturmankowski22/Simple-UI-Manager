using Zenject;

namespace SUIT.Components.Controllers
{
    public abstract class ControllerBase
    {
        [Inject] protected SignalBus _signalBus;
    }
}
using Zenject;

namespace SUIM.Components.Controllers
{
    public abstract class ControllerBase
    {
        [Inject] protected SignalBus _signalBus;
    }
}
using SUIT.Components.Controllers;
using SUIT.Utils;

namespace SUIT.Demo1
{
    public sealed class ViewADemoController : ControllerBase
    {
        public void OpenBView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewBDemo));
        }
    }
}
using SUIT.Components.Controllers;
using SUIT.Utils;

namespace SUIT.Demo1
{
    public sealed class ViewCDemoController : ControllerBase
    {
        public void OpenBView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewBDemo));
        }

        public void OpenDView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewDDemo));
        }
    }
}
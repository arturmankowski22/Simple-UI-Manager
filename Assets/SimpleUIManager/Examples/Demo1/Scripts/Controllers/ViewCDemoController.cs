using SUIM.Components.Controllers;
using SUIM.Utils;

namespace SUIM.Demo1
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
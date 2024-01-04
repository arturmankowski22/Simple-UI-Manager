using SUIM.Components.Controllers;
using SUIM.Utils;

namespace SUIM.Demo1
{
    public sealed class ViewADemoController : ControllerBase
    {
        public void OpenBView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewBDemo));
        }
    }
}
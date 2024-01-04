using SUIM.Components.Controllers;
using SUIM.Utils;

namespace SUIM.Demo1
{
    public sealed class ViewDDemoController : ControllerBase
    {
        public void OpenCView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewCDemo));
        }
    }
}
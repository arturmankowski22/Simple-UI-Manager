using SUIT.Components.Controllers;
using SUIT.Utils;

namespace SUIT.Demo1
{
    public sealed class ViewDDemoController : ControllerBase
    {
        public void OpenCView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewCDemo));
        }
    }
}
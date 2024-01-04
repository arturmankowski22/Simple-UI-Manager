using SUIM.Components.Controllers;
using SUIM.Utils;

namespace SUIM.Demo1
{
    public sealed class ViewBDemoController : ControllerBase
    {
        public void OpenAView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewADemo));
        }

        public void OpenCView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewCDemo));
        }

        public void OpenB1SubView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewB1Demo));
        }

        public void OpenB2SubView()
        {
            _signalBus.FireChangeViewRequest(typeof(ViewB2Demo));
        }
    }
}
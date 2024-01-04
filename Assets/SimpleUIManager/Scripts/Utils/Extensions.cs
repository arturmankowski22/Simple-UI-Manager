using System;
using SUIM.Signals;
using Zenject;

namespace SUIM.Utils
{
    public static class Extensions
    {
        public static void FireChangeViewRequest(this SignalBus signalBus, Type type)
        {
            signalBus.Fire(new OnChangeView { ViewToShow = type });
        }

        public static void FireShowPageAdditivelyRequest(this SignalBus signalBus, Type type)
        {
            signalBus.Fire(new OnShowViewAdditively { ViewToShow = type });
        }

        public static void FireHideGivenAdditivePageRequest(this SignalBus signalBus, Type type)
        {
            signalBus.Fire(new OnHideGivenAdditiveView { ViewToHide = type });
        }
    }
}
using System;
using SUIT.Signals;
using Zenject;

namespace SUIT.Utils
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

        public static void FireOnShowPreviousView(this SignalBus signalBus)
        {
            signalBus.Fire<OnShowPreviousView>();
        }
    }
}
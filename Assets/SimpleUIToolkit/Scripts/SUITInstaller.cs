using SUIT.Animation;
using SUIT.Signals;
using Zenject;

namespace SUIT
{
    public sealed class SUITInstaller : MonoInstaller<SUITInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IViewAnimationInvoker>().To<ViewAnimationInvoker>().AsSingle().NonLazy();
            DeclareSignals();
        }

        private void DeclareSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<OnChangeView>();
            Container.DeclareSignal<OnShowViewAdditively>();
            Container.DeclareSignal<OnHideCurrentView>();
            Container.DeclareSignal<OnHideLastAdditiveView>();
            Container.DeclareSignal<OnHideGivenAdditiveView>();
            Container.DeclareSignal<OnHideAllAdditiveViews>();
            Container.DeclareSignal<OnShowPreviousView>();
        }
    }
}
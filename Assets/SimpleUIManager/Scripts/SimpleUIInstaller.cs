using SUIM.Animation;
using SUIM.Signals;
using Zenject;

namespace SUIM
{
    public sealed class SimpleUIInstaller : MonoInstaller<SimpleUIInstaller>
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
        }
    }
}
using Zenject;

namespace SUIM.Demo1
{
    public class Demo1Installer : MonoInstaller<Demo1Installer>
    {
        public override void InstallBindings()
        {
            Container.Bind<ViewADemoController>().AsSingle();
            Container.Bind<ViewBDemoController>().AsSingle();
            Container.Bind<ViewCDemoController>().AsSingle();
            Container.Bind<ViewDDemoController>().AsSingle();
        }
    }
}
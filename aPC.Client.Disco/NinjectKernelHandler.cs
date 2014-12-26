using aPC.Client.Disco.Communication;
using aPC.Client.Disco.Generators;
using aPC.Common.Client;
using aPC.Common.Client.Communication;
using aPC.Common.Entities;
using Ninject;

namespace aPC.Client.Disco
{
  internal class NinjectKernelHandler
  {
    public StandardKernel Kernel { get; private set; }

    public NinjectKernelHandler()
    {
      Kernel = new StandardKernel();
      Kernel.Bind<Settings>().ToSelf().InSingletonScope();
      Kernel.Bind<HostnameAccessor>().ToSelf().InSingletonScope();
      Kernel.Bind<NotificationClientBase>().To<NotificationClient>();
      Kernel.Bind<IGenerator<amBXScene>>().To<RandomSceneGenerator>();
      Kernel.Bind<IGenerator<LightSection>>().To<RandomLightSectionGenerator>();
    }
  }
}
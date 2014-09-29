using aPC.Common.Client;
using aPC.Client.Disco.Communication;
using aPC.Client.Disco.Generators;
using aPC.Common.Entities;
using aPC.Common.Client.Communication;

namespace aPC.Client.Disco
{
  class NinjectKernelHandler : NinjectKernelHandlerBase
  {
    protected override void SetupBindings()
    {
      kernel.Bind<Settings>().ToSelf().InSingletonScope();
      kernel.Bind<HostnameAccessor>().ToSelf().InSingletonScope();
      kernel.Bind<NotificationClientBase>().To<NotificationClient>();
      kernel.Bind<IGenerator<amBXScene>>().To<RandomSceneGenerator>();
      kernel.Bind<IGenerator<LightSection>>().To<RandomLightSectionGenerator>();
    }
  }
}

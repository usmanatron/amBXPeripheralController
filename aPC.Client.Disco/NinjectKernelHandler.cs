using aPC.Client.Disco.Communication;
using aPC.Client.Disco.Generators;
using aPC.Common.Client;
using aPC.Common.Client.Communication;
using aPC.Common.Entities;

namespace aPC.Client.Disco
{
  internal class NinjectKernelHandler : NinjectKernelHandlerBase
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
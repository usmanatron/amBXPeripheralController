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
      mKernel.Bind<Settings>().ToSelf().InSingletonScope();
      mKernel.Bind<HostnameAccessor>().ToSelf().InSingletonScope();
      mKernel.Bind<NotificationClientBase>().To<NotificationClient>();
      mKernel.Bind<IGenerator<amBXScene>>().To<RandomSceneGenerator>();
      mKernel.Bind<IGenerator<LightSection>>().To<RandomLightSectionGenerator>();
    }
  }
}

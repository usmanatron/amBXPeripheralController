using System.Linq;
using aPC.Common.Communication;
using aPC.Common;
using aPC.Client.Disco.Communication;

namespace aPC.Client.Disco
{
  class NinjectKernelHandler : NinjectKernelHandlerBase
  {
    public NinjectKernelHandler(Settings xiSettings) : base ()
    {
      SetupBindings(xiSettings);
    }

    protected override void SetupBindings(params object[] xiParams)
    {
      mKernel.Bind<INotificationClient>().To<NotificationClient>();
      mKernel.Bind<Settings>().ToConstant((Settings) xiParams.Single());
    }
  }
}

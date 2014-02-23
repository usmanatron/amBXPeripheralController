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
      SetupSettingsBinding(xiSettings);
    }

    protected override void SetupBindings()
    {
      mKernel.Bind<INotificationClient>().To<NotificationClient>();
    }

    private void SetupSettingsBinding(Settings xiSettings)
    {
      mKernel.Bind<Settings>().ToConstant(xiSettings);
    }
  }
}

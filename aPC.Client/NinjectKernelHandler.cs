using aPC.Client.Communication;
using aPC.Common.Communication;
using aPC.Common.Client;

namespace aPC.Client
{
  class NinjectKernelHandler : NinjectKernelHandlerBase
  {
    public NinjectKernelHandler(Settings xiSettings)
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

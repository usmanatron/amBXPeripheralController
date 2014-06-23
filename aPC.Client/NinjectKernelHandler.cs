using aPC.Client.Communication;
using aPC.Common.Communication;
using aPC.Common.Client;
using aPC.Client.Console;
using Ninject;

namespace aPC.Client
{
  public sealed class NinjectKernelHandler
  {
    private NinjectKernelHandler()
    {
      Kernel = new StandardKernel();
      AddBindings();
    }

    private void AddBindings()
    {
      Kernel.Bind<Settings>().ToConstant(Settings.Instance);
      Kernel.Bind<INotificationClient>().To<NotificationClient>();
      Kernel.Bind<ConsoleRunner>().ToSelf();
    }

    public static NinjectKernelHandler Instance
    {
      get
      {
        return mInstance;
      }
    }

    private static NinjectKernelHandler mInstance = new NinjectKernelHandler();

    public StandardKernel Kernel;
  }
}

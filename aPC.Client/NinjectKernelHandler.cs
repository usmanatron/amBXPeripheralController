using aPC.Client.Communication;
using aPC.Common.Communication;
using aPC.Common.Client;
using aPC.Client.Console;
using Ninject;
using aPC.Client.Scene;

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
      Kernel.Bind<Settings>().ToSelf().InSingletonScope();
      Kernel.Bind<INotificationClient>().To<NotificationClient>();
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

using aPC.Client.Communication;
using aPC.Common.Communication;
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

      Kernel.Bind<IntegratedListing>().ToSelf().InSingletonScope();
      Kernel.Bind<CustomListing>().ToSelf().InSingletonScope();
    }

    public static NinjectKernelHandler Instance
    {
      get
      {
        return mInstance;
      }
    }

    private static readonly NinjectKernelHandler mInstance = new NinjectKernelHandler();
    public StandardKernel Kernel;
  }
}

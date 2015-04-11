using aPC.Common.Communication;
using aPC.Server.Communication;
using aPC.Server.Engine;
using Ninject;

namespace aPC.Server
{
  internal class Server
  {
    private static void Main()
    {
      var kernel = new StandardKernel();
      kernel.Bind<INotificationService>().To<NotificationService>();
      kernel.Bind<AmbxEngineWrapper>().ToSelf().InSingletonScope();

      var task = kernel.Get<ServerTask>();
      task.Run();
    }
  }
}
using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Server.Engine;
using aPC.Server.Communication;
using log4net;

namespace aPC.Server
{
  internal class Server
  {
    private static void Main(string[] args)
    {
      log.Info("Server Initialising...");

      var accessor = new SceneAccessor(new DefaultScenes());
      var initialEvent = accessor.GetScene("Server_Startup");
      var initialScene = accessor.GetScene("Rainbow");
      var status = new SceneStatus(initialScene.SceneType);

      ServerTask = new ServerTask(initialScene,
                                  initialEvent,
                                  status,
                                  new NotificationService(),
                                  new EngineManager());

      log.Info("Server Ready.");
      ServerTask.Run();
    }

    private static ILog log = LogManager.GetLogger("Server");
    internal static ServerTask ServerTask;
  }
}
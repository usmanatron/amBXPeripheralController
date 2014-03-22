using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Communication;
using aPC.Server.Engine;
using log4net;

namespace aPC.Server
{
  class Server
  {
    private static void Main(string[] args)
    {
      Log.Info("Server Initialising...");

      var lAccessor = new SceneAccessor();
      var lInitialEvent = lAccessor.GetScene("Server_Startup");
      var lInitialScene = lAccessor.GetScene("Rainbow");
      var lStatus = new SceneStatus(lInitialScene.SceneType);

      ServerTask = new ServerTask(lInitialScene, lInitialEvent, lStatus, new NotificationService(), new EngineManager());

      Log.Info("Server Ready.");
      ServerTask.Run();
    }

    private static ILog Log = LogManager.GetLogger("Server");
    internal static ServerTask ServerTask;
  }
}

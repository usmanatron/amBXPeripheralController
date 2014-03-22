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
      Log.Info("Starting Server...");

      var lInitialScene = new SceneAccessor().GetScene("Default_RedVsBlue");
      var lStatus = new SceneStatus(lInitialScene.SceneType);
      ServerTask = new ServerTask(lInitialScene, lStatus, new NotificationService(), new EngineManager());
      ServerTask.Run();
    }

    private static ILog Log = LogManager.GetLogger(typeof(Server));
    internal static ServerTask ServerTask;
  }
}

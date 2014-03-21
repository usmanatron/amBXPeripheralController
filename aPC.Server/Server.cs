using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Communication;
using aPC.Server.Engine;

namespace aPC.Server
{
  class Server
  {
    private static void Main(string[] args)
    {
      ServerTask = new ServerTask();
      var lInitialScene = new SceneAccessor().GetScene("Default_RedVsBlue");
      var lStatus = new SceneStatus(lInitialScene.SceneType);
      ServerTask = new ServerTask(lInitialScene, lStatus, new NotificationService(), new EngineManager());
      ServerTask.Run();
    }

    internal static ServerTask ServerTask;
  }
}

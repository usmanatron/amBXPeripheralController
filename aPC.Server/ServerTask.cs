using aPC.Common.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Server;
using aPC.Server.Communication;
using aPC.Server.Engine;
using System.Threading;

namespace aPC.Server
{
  internal class ServerTask
  {
    private static NewSceneProcessor newSceneProcessor;
    private readonly INotificationService notificationService;
    private readonly AmbxEngineWrapper engine;

    public ServerTask(NewSceneProcessor newSceneProcessor, INotificationService notificationService, AmbxEngineWrapper engine)
    {
      ServerTask.newSceneProcessor = newSceneProcessor;
      this.notificationService = notificationService;
      this.engine = engine;
    }

    public void Run()
    {
      using (new CommunicationManager(notificationService))
      using (engine)
      {
        var initialScene = new DefaultScenes().Rainbow;
        newSceneProcessor.Process(initialScene);

        while (true)
        {
          Thread.Sleep(10 * 1000);
        }
      }
    }

    public static void UpdateScene(amBXScene scene)
    {
      newSceneProcessor.Process(scene);
    }
  }
}
using aPC.Common.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Server.Communication;
using aPC.Server.Engine;
using System.Threading;

namespace aPC.Server
{
  internal class ServerTask
  {
    private static NewSceneProcessor newSceneProcessor;
    private static ExclusiveProcessor exclusiveProcessor;
    private readonly INotificationService notificationService;
    private readonly AmbxEngineWrapper engine;

    public ServerTask(NewSceneProcessor newSceneProcessor, ExclusiveProcessor exclusiveProcessor, INotificationService notificationService, AmbxEngineWrapper engine)
    {
      ServerTask.newSceneProcessor = newSceneProcessor;
      ServerTask.exclusiveProcessor = exclusiveProcessor;
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

    public static void Update(amBXScene scene)
    {
      newSceneProcessor.Process(scene);
    }

    public static void UpdateExclusive(Frame frame)
    {
      exclusiveProcessor.Process(frame);
    }
  }
}
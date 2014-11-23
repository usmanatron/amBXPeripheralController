using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Server.Communication;
using System.Threading;

namespace aPC.Server
{
  internal class ServerTask
  {
    private ConductorManager conductorManager;
    private amBXScene initialScene;
    private amBXScene initialEvent;
    private SceneUpdateHandler mSceneUpdateHandler;
    private ISceneStatus status;
    private INotificationService notificationService;
    private IEngine engine;

    public ServerTask(
      amBXScene initialScene,
      amBXScene initialEvent,
      ISceneStatus status,
      INotificationService notificationService,
      IEngine engine)
    {
      this.initialScene = initialScene;
      this.initialEvent = initialEvent;
      this.status = status;
      this.notificationService = notificationService;
      this.engine = engine;
    }

    internal void Run()
    {
      using (new CommunicationManager(notificationService))
      using (engine)
      {
        conductorManager = new ConductorManager(engine, initialScene, EventComplete);
        mSceneUpdateHandler = new SceneUpdateHandler(initialScene, initialEvent, conductorManager, status);

        while (true)
        {
          Thread.Sleep(10000);
        }
      }
    }

    internal void Update(amBXScene scene)
    {
      mSceneUpdateHandler.UpdateScene(scene);
    }

    /// <remarks>
    ///   This is called when an event has been completed and reinstates the previous behaviour before the event
    ///   was run.
    /// </remarks>
    internal void EventComplete()
    {
      mSceneUpdateHandler.UpdatePostEvent();
    }
  }
}
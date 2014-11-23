using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Server.Communication;
using System.Threading;

namespace aPC.Server
{
  internal class ServerTask
  {
    public ServerTask(
      amBXScene xiInitialScene,
      amBXScene xiInitialEvent,
      ISceneStatus xiStatus,
      INotificationService xiNotificationService,
      IEngine xiEngine)
    {
      mInitialScene = xiInitialScene;
      mInitialEvent = xiInitialEvent;
      mStatus = xiStatus;
      mNotificationService = xiNotificationService;
      mEngine = xiEngine;
    }

    internal void Run()
    {
      using (new CommunicationManager(mNotificationService))
      using (mEngine)
      {
        mConductorManager = new ConductorManager(mEngine, mInitialScene, EventComplete);
        mSceneUpdateHandler = new SceneUpdateHandler(mInitialScene, mInitialEvent, mConductorManager, mStatus);

        while (true)
        {
          Thread.Sleep(10000);
        }
      }
    }

    internal void Update(amBXScene xiScene)
    {
      mSceneUpdateHandler.UpdateScene(xiScene);
    }

    /// <remarks>
    ///   This is called when an event has been completed and reinstates the previous behaviour before the event
    ///   was run.
    /// </remarks>
    internal void EventComplete()
    {
      mSceneUpdateHandler.UpdatePostEvent();
    }

    private ConductorManager mConductorManager;
    private amBXScene mInitialScene;
    private amBXScene mInitialEvent;
    private SceneUpdateHandler mSceneUpdateHandler;
    private ISceneStatus mStatus;
    private INotificationService mNotificationService;
    private IEngine mEngine;
  }
}
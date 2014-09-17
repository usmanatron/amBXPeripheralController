using aPC.Common;
using aPC.Common.Entities;
using aPC.Server;
using aPC.Server.Communication;
using aPC.Server.Conductors;
using aPC.Server.Engine;
using aPC.Server.Actors;
using aPC.Server.SceneHandlers;
using System.Threading;
using aPC.Common.Communication;

namespace aPC.Server
{
  public class ServerTask
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

    public void Run()
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

    public void Update(amBXScene xiScene)
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
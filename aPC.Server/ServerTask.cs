using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Communication;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.EngineActors;
using aPC.Server.Communication;
using aPC.Common.Server.SceneHandlers;
using System.Threading;

namespace aPC.Server
{
  class ServerTask
  {
    public ServerTask()
    {
      mInitialScene = new SceneAccessor().GetScene("Default_RedVsBlue");
      mStatus = new SceneStatus(mInitialScene.SceneType);
    }

    internal void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        mConductorManager = new ConductorManager(lEngine, mInitialScene, EventComplete);
        mSceneUpdateHandler = new SceneUpdateHandler(mConductorManager, mStatus);
        mConductorManager.RunSynchronised();

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
    private SceneUpdateHandler mSceneUpdateHandler;
    private SceneStatus mStatus;
  }
}
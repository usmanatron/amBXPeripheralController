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
        mSceneUpdateHandler = new SceneUpdateHandler(mConductorManager);

        KickOffConductors();

        while (true)
        {
          Thread.Sleep(10000);
        }
      }
    }

    private void KickOffConductors()
    {
      if (mStatus.CurrentSceneType == eSceneType.Desync)
      {
        mConductorManager.RunAllManagersDeSynchronised();
      }
      else
      {
        mConductorManager.RunSynchronised();
      }
    }

    internal void Update(amBXScene xiScene)
    {
      mSceneUpdateHandler.UpdateScene(xiScene);
      
      mStatus.CurrentSceneType = xiScene.SceneType;
      KickOffConductors();
    }

    /// <remarks>
    ///   This is called when an event has been completed and reinstates the previous synchronicity before the event 
    ///   was run.  In the end, this is only important when the event causes a change in synchronicity 
    ///   (e.g. from desynchronised to synchronised).
    ///   Note that there is a potential issue here: if we ran a Desynchronised event we will call this method 
    ///   multiple times (once per light \ fan etc.).  mIsCurrentlyRunningEvent should however stop this 
    ///   being a problem?
    /// </remarks>
    internal void EventComplete()
    {
      mStatus.CurrentSceneType = mStatus.PreviousSceneType;
      // Look at SceneType and start the right set again
      if (mStatus.CurrentSceneType == eSceneType.Desync)
      {
        // Start the desync ones
      }
      else
      {
        // start the sync ones
      }
    }

    private ConductorManager mConductorManager;
    private amBXScene mInitialScene;
    private SceneUpdateHandler mSceneUpdateHandler;
    private SceneStatus mStatus;
  }
}
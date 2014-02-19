using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server;
using aPC.Common.Server.Communication;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.EngineActors;
using aPC.Server.Communication;
using System.Threading.Tasks;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Server
{
  class ServerTask
  {
    public ServerTask()
    {
      mSyncManager = new SynchronisationManager();
    }

    internal void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupSynchronisedManager(lEngine);
        mDesynchronisedManager = new ComponentManagerCollection(lEngine, EventComplete);

        // Start the default initial scene
        Update(new SceneAccessor().GetScene("Default_RedVsBlue"));

        while (true)
        {
          if (mSyncManager.IsSynchronised)
          {
            mSyncManager.RunWhileSynchronised(mFrame.Run);
          }
          else
          {
            mDesynchronisedManager.RunAllManagersDeSynchronised(mSyncManager);
          }
        }
      }
    }

    private void SetupSynchronisedManager(EngineManager xiEngine)
    {
      var lActor = new FrameActor(xiEngine);
      mFrame = new FrameConductor(lActor, new FrameHandler());
    }

    internal void Update(amBXScene xiScene)
    {
      if (xiScene.IsSynchronised)
      {
        mSyncManager.IsSynchronised = true;
      }
      else
      {
        mSyncManager.IsSynchronised = false;
      }

      UpdateActors(xiScene);
    }

    private void UpdateActors(amBXScene xiScene)
    {
      if (!xiScene.IsEvent)
      {
        UpdateSynchronisedActor(xiScene);
        UpdateUnsynchronisedActors(xiScene);
      }
      else 
      {
        mIsCurrentlyRunningEvent = true;

        if (mSyncManager.IsSynchronised)
        {
          UpdateSynchronisedActor(xiScene);
        }
        else
        {
          UpdateUnsynchronisedActors(xiScene);
        }
      }
    }

    private void UpdateSynchronisedActor(amBXScene xiScene)
    {
      mFrame.UpdateScene(xiScene);
    }

    private void UpdateUnsynchronisedActors(amBXScene xiScene)
    {
      mDesynchronisedManager.UpdateAllManagers(xiScene);
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
      if (mIsCurrentlyRunningEvent && mSyncManager.WasPreviouslySynchronised != mSyncManager.IsSynchronised)
      {
        mIsCurrentlyRunningEvent = false;
        mSyncManager.IsSynchronised = !mSyncManager.IsSynchronised;
      }
    }

    private FrameConductor mFrame;
    private ComponentManagerCollection mDesynchronisedManager;

    private readonly SynchronisationManager mSyncManager;
    private bool mIsCurrentlyRunningEvent;
  }
}
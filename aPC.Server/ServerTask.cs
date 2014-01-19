using aPC.Common;
using aPC.Common.Accessors;
using aPC.Common.Entities;
using aPC.Common.Server.Communication;
using aPC.Common.Server.Managers;
using aPC.Common.Server.EngineActors;
using aPC.Server.Communication;
using System.Threading.Tasks;

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
        mFrame = new FrameActor(lEngine, EventComplete);
        mDesynchronisedManager = new DesynchronisedActorManager(lEngine, EventComplete);

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
            Parallel.ForEach(mDesynchronisedManager.ActorsWithType(eActorType.Light), 
                             light => mSyncManager.RunWhileUnSynchronised(light.Actor.Run));
            /*
              qqUMI - BUG:
             *  In theory, we can change the above to use .AllActors and it will change everything.
             * In practice this doesn't work.  Problem can be reproduced by running Default_RedVsBlue followed
             * by CNet_FlashingYellow.  Currently, the FanManager blows up because the 2 repeatable frames have 
             * null for Fan data => null ref exception.  Need to enhance so that it does something sensible.
            */
          }
        }
      }
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
      mFrame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedActors(amBXScene xiScene)
    {
      Parallel.ForEach(mDesynchronisedManager.ActorsWithType(eActorType.Light),
                       light => light.Actor.UpdateManager(xiScene));
      //qqUMI Not working yet.  See line 40
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

    private FrameActor mFrame;
    private DesynchronisedActorManager mDesynchronisedManager;

    private readonly SynchronisationManager mSyncManager;
    private bool mIsCurrentlyRunningEvent;
  }
}
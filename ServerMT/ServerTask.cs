using System.Collections.Generic;
using Common.Server.Communication;
using ServerMT.Communication;
using System.Linq;
using System.Threading.Tasks;
using amBXLib;
using Common.Server.Managers;
using Common.Server.Applicators;
using ServerMT.Applicators;
using Common.Entities;
using Common.Accessors;
using System;

namespace ServerMT
{
  class ServerTask
  {
    public ServerTask()
    {
      mLights = new Dictionary<CompassDirection, LightApplicator>();
      mFans = new Dictionary<CompassDirection, FanApplicator>();
      mSyncManager = new SynchronisationManager();
    }

    internal void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupApplicators(lEngine);

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
            Parallel.ForEach(mLights, light => mSyncManager.RunWhileUnSynchronised(light.Value.Run));
            
            //qqUMI Fan \ Rumble support missing.
          }
        }
      }
    }

    private void SetupApplicators(EngineManager xiEngine)
    {
      mFrame = new FrameApplicator(xiEngine, EventComplete);

      mLights.Add(CompassDirection.North,     new LightApplicator(CompassDirection.North, xiEngine, EventComplete));
      mLights.Add(CompassDirection.NorthEast, new LightApplicator(CompassDirection.NorthEast, xiEngine, EventComplete));
      mLights.Add(CompassDirection.East,      new LightApplicator(CompassDirection.East, xiEngine, EventComplete));
      mLights.Add(CompassDirection.SouthEast, new LightApplicator(CompassDirection.SouthEast, xiEngine, EventComplete));
      mLights.Add(CompassDirection.South,     new LightApplicator(CompassDirection.South, xiEngine, EventComplete));
      mLights.Add(CompassDirection.SouthWest, new LightApplicator(CompassDirection.SouthWest, xiEngine, EventComplete));
      mLights.Add(CompassDirection.West,      new LightApplicator(CompassDirection.West, xiEngine, EventComplete));
      mLights.Add(CompassDirection.NorthWest, new LightApplicator(CompassDirection.NorthWest, xiEngine, EventComplete));

      mFans.Add(CompassDirection.East, new FanApplicator(CompassDirection.East, xiEngine, EventComplete));
      mFans.Add(CompassDirection.West, new FanApplicator(CompassDirection.West, xiEngine, EventComplete));

      //qqUMI Add Rumble here
    }

    /*qqUMI
 * 
 * There are a number of issues here:
 * * If we're in de-sync mode and run a synchronised event, we'll fall back to whatever the 
 *   --synchronised-- previous scene was - not ideal
 *   
 * Fixing this may just be a case of changing how UpdateManager works a little bit to allow
 * us to force a value for IsDormant?
 */
    internal void Update(amBXScene xiScene)
    {
      var lWasSynchronised = mSyncManager.IsSynchronised;

      if (xiScene.IsSynchronised)
      {
        mSyncManager.IsSynchronised = true;
      }
      else
      {
        mSyncManager.IsSynchronised = false;
      }

      if (!xiScene.IsEvent)
      {
        UpdateSynchronisedApplicator(xiScene);
        UpdateUnsynchronisedElements(xiScene);
      }
      else 
      {
        if (mSyncManager.IsSynchronised)
        {
          UpdateSynchronisedApplicator(xiScene);
        }
        else
        {
          UpdateUnsynchronisedElements(xiScene)
        }
      }
    }

    internal void EventComplete()
    {
      // If the event is sync and the previous was sync, then at this point we do nothing, as the Manager will take care of everything
      // Similarly if the event and the previous was desync.

      // If, however, one is sync and the other is desync, then it ges complicated.
      // In this case, we change SyncManager.IsSynchronised appropriately to swap control.

      /* Case 1 - Previously was desync and we run a sync event
       *   Swapping IsScynchronised is enough, PROVIDED we change update to not add the event to the desync components.
       * 
       * Case 2 - Previously was sync and we run a desync event
       *   A bit dirty - in theory the same as case 1 however it may not finish cleanly (very much a fringe case though)
       * 
       */

      //1. check if we need to do anything by confirming we changed sync-ness due to the event

      //2. If we have change IsSynchronised.  If not, do nothing.
    }

    private void UpdateSynchronisedApplicator(amBXScene xiScene)
    {
      mFrame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedElements(amBXScene xiScene)
    {
      foreach (var lLight in mLights)
      {
        lLight.Value.UpdateManager(xiScene);
      }

      foreach (var lFan in mFans)
      {
        lFan.Value.UpdateManager(xiScene);
      }

      //qqUMI Rumble not supported yet
    }


    private SynchronisationManager mSyncManager;

    private FrameApplicator mFrame;
    private Dictionary<CompassDirection, LightApplicator> mLights;
    private Dictionary<CompassDirection, FanApplicator> mFans;
    //private Dictionary<CompassDirection, RumbleApplicator> mRumbles;
  }
}
using aPC.Common.Accessors;
using aPC.Common.Entities;
using aPC.Common.Server.Communication;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Applicators;
using aPC.Server.Applicators;
using aPC.Server.Communication;
using amBXLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace aPC.Server
{
  class ServerTask
  {
    public ServerTask()
    {
      mLights = new Dictionary<CompassDirection, LightApplicator>();
      mFans = new Dictionary<CompassDirection, FanApplicator>();
      mRumbles = new Dictionary<CompassDirection, RumbleApplicator>();
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
            ThreadPool.QueueUserWorkItem(_ => Parallel.ForEach(mLights, light => mSyncManager.RunWhileUnSynchronised(light.Value.Run)));
            ThreadPool.QueueUserWorkItem(_ => Parallel.ForEach(mFans, fan => mSyncManager.RunWhileUnSynchronised(fan.Value.Run)));
            ThreadPool.QueueUserWorkItem(_ => Parallel.ForEach(mLights, rumble => mSyncManager.RunWhileUnSynchronised(rumble.Value.Run)));
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

      mRumbles.Add(CompassDirection.Everywhere, new RumbleApplicator(CompassDirection.Everywhere, xiEngine, EventComplete));
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

      UpdateApplicators(xiScene);
    }

    private void UpdateApplicators(amBXScene xiScene)
    {
      if (!xiScene.IsEvent)
      {
        UpdateSynchronisedApplicator(xiScene);
        UpdateUnsynchronisedElements(xiScene);
      }
      else 
      {
        mIsCurrentlyRunningEvent = true;

        if (mSyncManager.IsSynchronised)
        {
          UpdateSynchronisedApplicator(xiScene);
        }
        else
        {
          UpdateUnsynchronisedElements(xiScene);
        }
      }
    }

    private void UpdateSynchronisedApplicator(amBXScene xiScene)
    {
      mFrame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedElements(amBXScene xiScene)
    {
      Parallel.ForEach<KeyValuePair<CompassDirection, LightApplicator>>(mLights, light => light.Value.UpdateManager(xiScene));
      Parallel.ForEach<KeyValuePair<CompassDirection, FanApplicator>>(mFans, fan => fan.Value.UpdateManager(xiScene));
      Parallel.ForEach<KeyValuePair<CompassDirection, RumbleApplicator>>(mRumbles, rumble => rumble.Value.UpdateManager(xiScene));
    }

    /// <remarks>
    ///   This is called when an event has been completed and reinstates the previous synchronicity before the event 
    ///   was run.  In the end, this is only important when the event causes a change in synchronicity 
    ///   (e.g. from desynchronised to synchronised)
    /// </remarks>
    internal void EventComplete()
    {
      // qqUMI potential limitation: if we have a desync. event, we will hit this many times (one per light \ fan \ rumbler).
      // May not be a problem because of mIsCurrentlyRunningEvent?
      if (mIsCurrentlyRunningEvent && mSyncManager.WasPreviouslySynchronised != mSyncManager.IsSynchronised)
      {
        mIsCurrentlyRunningEvent = false;
        mSyncManager.IsSynchronised = !mSyncManager.IsSynchronised;
      }
    }

    private FrameApplicator mFrame;
    private Dictionary<CompassDirection, LightApplicator> mLights;
    private Dictionary<CompassDirection, FanApplicator> mFans;
    private Dictionary<CompassDirection, RumbleApplicator> mRumbles;

    private SynchronisationManager mSyncManager;
    private bool mIsCurrentlyRunningEvent;
  }
}
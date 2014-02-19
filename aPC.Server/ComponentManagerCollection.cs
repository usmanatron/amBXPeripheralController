using amBXLib;
using aPC.Common.Server.EngineActors;
using aPC.Server.EngineActors;
using aPC.Server.Managers;
using aPC.Common.Server.Managers;
using aPC.Common.Server;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Server.Snapshots;
using System.Threading;

namespace aPC.Server
{
  class ComponentManagerCollection
  {
    public ComponentManagerCollection(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupManagers(xiEngine, xiEventComplete);
    }

    private void SetupManagers(EngineManager xiEngine, Action xiAction)
    {
      mLightManagers = new List<LightConductor>();
      mFanManagers = new List<FanConductor>();
      mRumbleManagers = new List<RumbleConductor>();

      mLightManagers.Add(new LightConductor(eDirection.North, new LightActor(eDirection.North, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.NorthEast, new LightActor(eDirection.NorthEast, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.East, new LightActor(eDirection.East, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.SouthEast, new LightActor(eDirection.SouthEast, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.South, new LightActor(eDirection.South, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.SouthWest, new LightActor(eDirection.SouthWest, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.West, new LightActor(eDirection.West, xiEngine), xiAction));
      mLightManagers.Add(new LightConductor(eDirection.NorthWest, new LightActor(eDirection.NorthWest, xiEngine), xiAction));

      mFanManagers.Add(new FanConductor(eDirection.East, new FanActor(eDirection.East, xiEngine), xiAction));
      mFanManagers.Add(new FanConductor(eDirection.West, new FanActor(eDirection.West, xiEngine), xiAction));

      mRumbleManagers.Add(new RumbleConductor(eDirection.Center , new RumbleActor(eDirection.Center, xiEngine), xiAction));
    }

    public void RunAllManagersDeSynchronised(SynchronisationManager xiSyncManager)
    {
      bool lLighstsFinished, lFansFinished, lRumblesFinished = false;

      lLighstsFinished = RunTaskOnLights(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));
      lFansFinished = RunTaskOnFans(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));
      lRumblesFinished = RunTaskOnRumbles(manager => xiSyncManager.RunWhileUnSynchronised(manager.Run));

      while (!(lLighstsFinished && lFansFinished && lRumblesFinished))
      {
        Thread.Sleep(1000);
      }
    }

    public void UpdateAllManagers(amBXScene xiScene)
    {
      bool lLighstsFinished, lFansFinished, lRumblesFinished = false;

      lLighstsFinished = RunTaskOnLights(manager => manager.UpdateScene(xiScene));
      lFansFinished = RunTaskOnFans(manager => manager.UpdateScene(xiScene));
      lRumblesFinished = RunTaskOnRumbles(manager => manager.UpdateScene(xiScene));

      while (!(lLighstsFinished && lFansFinished && lRumblesFinished))
      {
        Thread.Sleep(1000);
      }
    }

    private bool RunTaskOnLights(Action<LightConductor> xiAction)
    {
      Parallel.ForEach(mLightManagers, manager => xiAction(manager));
      return true;
    }

    private bool RunTaskOnFans(Action<FanConductor> xiAction)
    {
      Parallel.ForEach(mFanManagers, manager => xiAction(manager));
      return true;
    }

    private bool RunTaskOnRumbles(Action<RumbleConductor> xiAction)
    {
      Parallel.ForEach(mRumbleManagers, manager => xiAction(manager));
      return true;
    }

    private List<LightConductor> mLightManagers;
    private List<FanConductor> mFanManagers;
    private List<RumbleConductor> mRumbleManagers;
  }
}

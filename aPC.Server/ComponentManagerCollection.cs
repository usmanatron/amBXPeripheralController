using amBXLib;
using aPC.Common.Server.EngineActors;
using aPC.Server.EngineActors;
using aPC.Server.Managers;
using aPC.Common.Server.Managers;
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
      mLightManagers = new List<LightManager>();
      mFanManagers = new List<FanManager>();
      mRumbleManagers = new List<RumbleManager>();

      mLightManagers.Add(new LightManager(eDirection.North, new LightActor(eDirection.North, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.NorthEast, new LightActor(eDirection.NorthEast, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.East, new LightActor(eDirection.East, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.SouthEast, new LightActor(eDirection.SouthEast, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.South, new LightActor(eDirection.South, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.SouthWest, new LightActor(eDirection.SouthWest, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.West, new LightActor(eDirection.West, xiEngine), xiAction));
      mLightManagers.Add(new LightManager(eDirection.NorthWest, new LightActor(eDirection.NorthWest, xiEngine), xiAction));

      mFanManagers.Add(new FanManager(eDirection.East, new FanActor(eDirection.East, xiEngine), xiAction));
      mFanManagers.Add(new FanManager(eDirection.West, new FanActor(eDirection.West, xiEngine), xiAction));

      mRumbleManagers.Add(new RumbleManager(eDirection.Center , new RumbleActor(eDirection.Center, xiEngine), xiAction));
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

    private bool RunTaskOnLights(Action<LightManager> xiAction)
    {
      Parallel.ForEach(mLightManagers, manager => xiAction(manager));
      return true;
    }

    private bool RunTaskOnFans(Action<FanManager> xiAction)
    {
      Parallel.ForEach(mFanManagers, manager => xiAction(manager));
      return true;
    }

    private bool RunTaskOnRumbles(Action<RumbleManager> xiAction)
    {
      Parallel.ForEach(mRumbleManagers, manager => xiAction(manager));
      return true;
    }

    private List<LightManager> mLightManagers;
    private List<FanManager> mFanManagers;
    private List<RumbleManager> mRumbleManagers;
  }
}

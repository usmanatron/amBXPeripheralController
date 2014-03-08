using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Entities;
using aPC.Common;

namespace aPC.Server
{
  class SceneUpdateHandler
  {
    public SceneUpdateHandler(ConductorManager xiConductorManager, SceneStatus xiStatus)
    {
      mConductorManager = xiConductorManager;
      mStatus = xiStatus;
    }

    public void UpdateScene(amBXScene xiScene)
    {
      mStatus.CurrentSceneType = xiScene.SceneType;
      UpdateActors(xiScene);
      KickOffConductors();
    }

    private void UpdateActors(amBXScene xiScene)
    {
      switch (xiScene.SceneType)
      {
        case eSceneType.Desync:
        case eSceneType.Sync:
          UpdateSynchronisedActor(xiScene);
          UpdateDesynchronisedActors(xiScene);
          break;
        case eSceneType.Event:
          UpdateSynchronisedActor(xiScene);
          break;
        default:
          throw new InvalidOperationException("Unexpected Scene Type.");
      }
    }

    private void UpdateSynchronisedActor(amBXScene xiScene)
    {
      mConductorManager.UpdateSync(xiScene);
    }

    private void UpdateDesynchronisedActors(amBXScene xiScene)
    {
      mConductorManager.UpdateDeSync(xiScene);
    }

    public void KickOffConductors()
    {
      if (mStatus.CurrentSceneType == mStatus.PreviousSceneType)
      {
        // Still running!
        return;
      }

      if (mStatus.CurrentSceneType == eSceneType.Desync)
      {
        mConductorManager.DisableSync();
        mConductorManager.RunDesynchronised();
      }
      else
      {
        mConductorManager.DisableDesync();
        mConductorManager.RunSynchronised();
      }
    }

    private ConductorManager mConductorManager;
    private SceneStatus mStatus;
  }
}

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
      Update(xiScene);
      UpdateActors(xiScene);
      KickOffConductors();
    }

    /// <summary>
    /// Does the necessary update steps
    /// </summary>
    /// <remarks>
    /// Fairly complex, as heavily dependent on the previous and (now)
    /// current scene types.  The method used for writing this out
    /// is a bit long-winded (hopefully to mkae it a bit clearer!)
    /// </remarks>
    private void Update(amBXScene xiScene)
    {
      switch (mStatus.PreviousSceneType)
      {
        case eSceneType.Desync:
          switch (mStatus.CurrentSceneType)
          {
            case eSceneType.Desync:
              UpdateDesynchronisedActors(xiScene);
              EnableDesynchronisedActors();
              break;
            case eSceneType.Sync:
              UpdateDesynchronisedActors(xiScene);
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActors();
              DisableDesynchronisedActors();
              break;
            case eSceneType.Event:
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActors();
              DisableDesynchronisedActors();
              break;
          }
          break;
        case eSceneType.Sync:
          switch (mStatus.CurrentSceneType)
          {
            case eSceneType.Desync:
              UpdateDesynchronisedActors(xiScene);
              EnableDesynchronisedActors();
              DisableSynchronisedActors();
            case eSceneType.Sync:
              UpdateSynchronisedActor(xiScene);
              UpdateDesynchronisedActors(xiScene);
              EnableSynchronisedActor();
            case eSceneType.Event:
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActor();
          }
          break;
        case eSceneType.Event:
          switch (mStatus.CurrentSceneType)
          {
            case eSceneType.Desync:
              EnableDesynchronisedActors();
              DisableSynchronisedActors();
            case eSceneType.Sync:
              EnableSynchronisedActors();
            case eSceneType.Event:
              // Event -> Event is allowed and overwrites the previous event
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActors();
          }
          break;
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

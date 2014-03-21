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
    public SceneUpdateHandler(ConductorManager xiConductorManager, ISceneStatus xiStatus)
    {
      mConductorManager = xiConductorManager;
      mStatus = xiStatus;
    }

    public void UpdateScene(amBXScene xiScene)
    {
      mStatus.CurrentSceneType = xiScene.SceneType;
      Update(xiScene);
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
              EnableSynchronisedActor();
              DisableDesynchronisedActors();
              break;
            case eSceneType.Event:
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActor();
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
              DisableSynchronisedActor();
              break;
            case eSceneType.Sync:
              UpdateSynchronisedActor(xiScene);
              UpdateDesynchronisedActors(xiScene);
              EnableSynchronisedActor();
              break;
            case eSceneType.Event:
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActor();
              break;
          }
          break;
        case eSceneType.Event:
          switch (mStatus.CurrentSceneType)
          {
            case eSceneType.Event:
              // Event -> Event is allowed and overwrites the previous event
              UpdateSynchronisedActor(xiScene);
              EnableSynchronisedActor();
              break;
            default:
              throw new InvalidOperationException("Attempted to transition from an event to another type.  this is unsupported for the Update method - please use UpdatePostEvent instead.");
          }
          break;
      }
    }

    /// <summary>
    /// Do the required operations to get everything moving post-scene
    /// </summary>
    public void UpdatePostEvent()
    {
      mStatus.CurrentSceneType = mStatus.PreviousSceneType;
      switch (mStatus.CurrentSceneType)
      {
        case eSceneType.Desync:
          EnableDesynchronisedActors();
          DisableSynchronisedActor();
          break;
        case eSceneType.Sync:
          EnableSynchronisedActor();
          break;
        case eSceneType.Event:
          throw new InvalidOperationException("Transitioning from an event to another event (after the event has finished) is unsupported and indicates a bug!");
      }
    }

    #region Sync \ Events

    private void UpdateSynchronisedActor(amBXScene xiScene)
    {
      mConductorManager.UpdateSync(xiScene);
    }

    private void EnableSynchronisedActor()
    {
      mConductorManager.EnableSync();
    }

    private void DisableSynchronisedActor()
    {
      mConductorManager.DisableSync();
    }

    #endregion

    #region Desync

    private void UpdateDesynchronisedActors(amBXScene xiScene)
    {
      mConductorManager.UpdateDeSync(xiScene);
    }

    private void EnableDesynchronisedActors()
    {
      mConductorManager.EnableDesync();
    }

    private void DisableDesynchronisedActors()
    {
      mConductorManager.DisableDesync();
    }

    #endregion

    private ConductorManager mConductorManager;
    private ISceneStatus mStatus;
  }
}

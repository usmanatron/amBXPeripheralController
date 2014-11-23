using aPC.Common;
using aPC.Common.Entities;
using System;

namespace aPC.Server
{
  internal class SceneUpdateHandler
  {
    private ConductorManager conductorManager;
    private ISceneStatus status;

    public SceneUpdateHandler(amBXScene initialScene, amBXScene initialEvent,
      ConductorManager conductorManager, ISceneStatus status)
    {
      this.conductorManager = conductorManager;
      this.status = status;
      InitialiseConductors(initialScene, initialEvent);
    }

    /// <summary>
    /// Starts up all conductors and pushes the intial scenes:
    /// * An event to be played on the Sync Conductor, followed by
    /// * A repeated scene to be played on the Desync Conductors
    /// </summary>
    private void InitialiseConductors(amBXScene initialScene, amBXScene initialEvent)
    {
      UpdateDesynchronisedActors(initialScene);
      EnableDesynchronisedActors();

      UpdateScene(initialEvent);
    }

    public void UpdateScene(amBXScene scene)
    {
      status.CurrentSceneType = scene.SceneType;
      Update(scene);
    }

    /// <summary>
    /// Does the necessary update steps
    /// </summary>
    /// <remarks>
    /// Fairly complex, as heavily dependent on the previous and (now)
    /// current scene types.  The method used for writing this out
    /// is a bit long-winded (hopefully to make it a bit clearer!)
    /// </remarks>
    private void Update(amBXScene scene)
    {
      switch (status.PreviousSceneType)
      {
        case eSceneType.Desync:
          switch (status.CurrentSceneType)
          {
            case eSceneType.Desync:
              UpdateDesynchronisedActors(scene);
              EnableDesynchronisedActors();
              break;
            case eSceneType.Sync:
              UpdateDesynchronisedActors(scene);
              UpdateSynchronisedActor(scene);
              EnableSynchronisedActor();
              DisableDesynchronisedActors();
              break;
            case eSceneType.Event:
              UpdateSynchronisedActor(scene);
              EnableSynchronisedActor();
              DisableDesynchronisedActors();
              break;
          }
          break;
        case eSceneType.Sync:
          switch (status.CurrentSceneType)
          {
            case eSceneType.Desync:
              UpdateDesynchronisedActors(scene);
              EnableDesynchronisedActors();
              DisableSynchronisedActor();
              break;
            case eSceneType.Sync:
              UpdateSynchronisedActor(scene);
              UpdateDesynchronisedActors(scene);
              EnableSynchronisedActor();
              break;
            case eSceneType.Event:
              UpdateSynchronisedActor(scene);
              EnableSynchronisedActor();
              break;
          }
          break;
        case eSceneType.Event:
          switch (status.CurrentSceneType)
          {
            case eSceneType.Event:
              // Event -> Event is allowed and overwrites the previous event
              UpdateSynchronisedActor(scene);
              EnableSynchronisedActor();
              break;
            default:
              throw new InvalidOperationException("Attempted to transition from an event to another type.  this is unsupported for the Update method - please use UpdatePostEvent instead.");
          }
          break;
      }
    }

    /// <summary>
    /// Do the required operations to get everything moving post-event
    /// </summary>
    public void UpdatePostEvent()
    {
      status.CurrentSceneType = status.PreviousSceneType;
      switch (status.CurrentSceneType)
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

    private void UpdateSynchronisedActor(amBXScene scene)
    {
      conductorManager.UpdateSync(scene);
    }

    private void EnableSynchronisedActor()
    {
      conductorManager.EnableSync();
    }

    private void DisableSynchronisedActor()
    {
      conductorManager.DisableSync();
    }

    #endregion Sync \ Events

    #region Desync

    private void UpdateDesynchronisedActors(amBXScene scene)
    {
      conductorManager.UpdateDeSync(scene);
    }

    private void EnableDesynchronisedActors()
    {
      conductorManager.EnableDesync();
    }

    private void DisableDesynchronisedActors()
    {
      conductorManager.DisableDesync();
    }

    #endregion Desync
  }
}
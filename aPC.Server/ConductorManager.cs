using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.SceneHandlers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.Server
{
  public class ConductorManager
  {
    protected FrameConductor frameConductor;
    protected List<ComponentConductor> desyncConductors;

    public ConductorManager(IEngine xiEngine, amBXScene scene, Action eventComplete)
    {
      desyncConductors = new List<ComponentConductor>();
      SetupManagers(xiEngine, scene, eventComplete);
    }

    private void SetupManagers(IEngine engine, amBXScene scene, Action action)
    {
      frameConductor = new FrameConductor(new FrameActor(engine), new FrameHandler(scene, action));

      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.North, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.NorthEast, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.East, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.SouthEast, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.South, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.SouthWest, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.West, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Light, eDirection.NorthWest, new ComponentActor(eComponentType.Light, engine), new LightHandler(scene, action)));

      desyncConductors.Add(new ComponentConductor(eComponentType.Fan, eDirection.East, new ComponentActor(eComponentType.Fan, engine), new FanHandler(scene, action)));
      desyncConductors.Add(new ComponentConductor(eComponentType.Fan, eDirection.West, new ComponentActor(eComponentType.Fan, engine), new FanHandler(scene, action)));

      desyncConductors.Add(new ComponentConductor(eComponentType.Rumble, eDirection.Center, new ComponentActor(eComponentType.Rumble, engine), new RumbleHandler(scene, action)));
    }

    #region Update Scene

    public void UpdateSync(amBXScene scene)
    {
      frameConductor.UpdateScene(scene);
    }

    public void UpdateDeSync(amBXScene scene)
    {
      Parallel.ForEach(desyncConductors, conductor => UpdateSceneIfRelevant(conductor, scene));
    }

    private void UpdateSceneIfRelevant(ComponentConductor conductor, amBXScene scene)
    {
      if (IsApplicableForConductor(scene.FrameStatistics, conductor.ComponentType, conductor.Direction))
      {
        conductor.UpdateScene(scene);
      }
    }

    private Func<FrameStatistics, eComponentType, eDirection, bool> IsApplicableForConductor =
      (statistics, componentType, direction) => statistics.AreEnabledForComponentAndDirection(componentType, direction);

    #endregion Update Scene

    public void EnableSync()
    {
      EnableAndRunIfRequired(frameConductor);
    }

    public void EnableDesync()
    {
      desyncConductors.ForEach(conductor => EnableAndRunIfRequired(conductor));
    }

    private void EnableAndRunIfRequired(IConductor conductor)
    {
      conductor.Enable();

      if (!conductor.IsRunning.Get)
      {
        ThreadPool.QueueUserWorkItem(_ => conductor.Run());
      }
    }

    public void DisableSync()
    {
      frameConductor.Disable();
    }

    public void DisableDesync()
    {
      desyncConductors.ForEach(conductor => conductor.Disable());
    }
  }
}
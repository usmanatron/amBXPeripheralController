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
    protected List<IConductor> desyncConductors;

    public ConductorManager(IEngine xiEngine, amBXScene scene, Action eventComplete)
    {
      desyncConductors = new List<IConductor>();
      SetupManagers(xiEngine, scene, eventComplete);
    }

    private void SetupManagers(IEngine engine, amBXScene scene, Action action)
    {
      frameConductor = new FrameConductor(new FrameActor(engine), new FrameHandler(scene, action));

      desyncConductors.Add(new LightConductor(eDirection.North, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.NorthEast, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.East, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.SouthEast, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.South, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.SouthWest, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.West, new ComponentActor<Light>(engine), new LightHandler(scene, action)));
      desyncConductors.Add(new LightConductor(eDirection.NorthWest, new ComponentActor<Light>(engine), new LightHandler(scene, action)));

      desyncConductors.Add(new FanConductor(eDirection.East, new ComponentActor<Fan>(engine), new FanHandler(scene, action)));
      desyncConductors.Add(new FanConductor(eDirection.West, new ComponentActor<Fan>(engine), new FanHandler(scene, action)));

      desyncConductors.Add(new RumbleConductor(eDirection.Center, new ComponentActor<Rumble>(engine), new RumbleHandler(scene, action)));
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

    private void UpdateSceneIfRelevant(IConductor conductor, amBXScene scene)
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
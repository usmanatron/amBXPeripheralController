using aPC.Server;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using aPC.Server.Conductors;
using aPC.Server.Engine;
using aPC.Server.Actors;
using aPC.Server.SceneHandlers;

namespace aPC.Server
{
  public class ConductorManager
  {
    public ConductorManager(IEngine xiEngine, amBXScene xiScene, Action xiEventComplete)
    {
      mDesyncConductors = new List<IConductor>();
      SetupManagers(xiEngine, xiScene, xiEventComplete);
    }

    private void SetupManagers(IEngine xiEngine, amBXScene xiScene, Action xiAction)
    {
      mFrameConductor = new FrameConductor(new FrameActor(xiEngine), new FrameHandler(xiScene, xiAction));      

      mDesyncConductors.Add(new LightConductor(eDirection.North, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.NorthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.East, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.SouthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.South, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.SouthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.West, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new LightConductor(eDirection.NorthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));

      mDesyncConductors.Add(new FanConductor(eDirection.East, new FanActor(xiEngine), new FanHandler(xiScene, xiAction)));
      mDesyncConductors.Add(new FanConductor(eDirection.West, new FanActor(xiEngine), new FanHandler(xiScene, xiAction)));

      mDesyncConductors.Add(new RumbleConductor(eDirection.Center, new RumbleActor(xiEngine), new RumbleHandler(xiScene, xiAction)));
    }

    #region Update Scene

    public void UpdateSync(amBXScene xiScene)
    {
      mFrameConductor.UpdateScene(xiScene);
    }
    
    public void UpdateDeSync(amBXScene xiScene)
    {
      Parallel.ForEach(mDesyncConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
    }

    private void UpdateSceneIfRelevant(IConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType, xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private Func<FrameStatistics, eComponentType, eDirection, bool> IsApplicableForConductor =
      (statistics, componentType, direction) => statistics.AreEnabledForComponentAndDirection(componentType, direction);

    #endregion

    public void EnableSync()
    {
      EnableAndRunIfRequired(mFrameConductor);
    }

    public void EnableDesync()
    {
      mDesyncConductors.ForEach(light => EnableAndRunIfRequired(light));
    }

    private void EnableAndRunIfRequired(IConductor xiConductor)
    {
      xiConductor.Enable();
      if (!xiConductor.IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => xiConductor.Run());
      }
    }

    public void DisableSync()
    {
      mFrameConductor.Disable();
    }

    public void DisableDesync()
    {
      mDesyncConductors.ForEach(light => light.Disable());
    }

    protected FrameConductor mFrameConductor;
    protected List<IConductor> mDesyncConductors;
  }
}

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
      mLightConductors = new List<LightConductor>();
      mFanConductors = new List<FanConductor>();
      mRumbleConductors = new List<RumbleConductor>();

      SetupManagers(xiEngine, xiScene, xiEventComplete);
    }

    private void SetupManagers(IEngine xiEngine, amBXScene xiScene, Action xiAction)
    {
      mFrameConductor = new FrameConductor(new FrameActor(xiEngine), new FrameHandler(xiScene, xiAction));      

      mLightConductors.Add(new LightConductor(eDirection.North, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.NorthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.East, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.SouthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.South, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.SouthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.West, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.NorthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));

      mFanConductors.Add(new FanConductor(eDirection.East, new FanActor(xiEngine), new FanHandler(xiScene, xiAction)));
      mFanConductors.Add(new FanConductor(eDirection.West, new FanActor(xiEngine), new FanHandler(xiScene, xiAction)));

      mRumbleConductors.Add(new RumbleConductor(eDirection.Center, new RumbleActor(xiEngine), new RumbleHandler(xiScene, xiAction)));
    }

    #region Update Scene

    public void UpdateSync(amBXScene xiScene)
    {
      mFrameConductor.UpdateScene(xiScene);
    }
    
    public void UpdateDeSync(amBXScene xiScene)
    {
      Parallel.ForEach(mLightConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
      Parallel.ForEach(mFanConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
      Parallel.ForEach(mRumbleConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
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
      mLightConductors.ForEach(light => ThreadPool.QueueUserWorkItem(_ => EnableAndRunIfRequired(light)));
      mFanConductors.ForEach(fan => ThreadPool.QueueUserWorkItem(_ => EnableAndRunIfRequired(fan)));
      mRumbleConductors.ForEach(rumble => ThreadPool.QueueUserWorkItem(_ => EnableAndRunIfRequired(rumble)));
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
      mLightConductors.ForEach(light => ThreadPool.QueueUserWorkItem(_ => light.Disable()));
      mFanConductors.ForEach(fan => ThreadPool.QueueUserWorkItem(_ => fan.Disable()));
      mRumbleConductors.ForEach(rumble => ThreadPool.QueueUserWorkItem(_ => rumble.Disable()));
    }

    protected FrameConductor mFrameConductor;
    protected List<LightConductor> mLightConductors;
    protected List<FanConductor> mFanConductors;
    protected List<RumbleConductor> mRumbleConductors;
  }
}

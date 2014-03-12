using aPC.Server.EngineActors;
using aPC.Server.Conductors;
using aPC.Common.Server;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using aPC.Server.SceneHandlers;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.SceneHandlers;

namespace aPC.Server
{
  class ConductorManager
  {
    public ConductorManager(EngineManager xiEngine, amBXScene xiScene, Action xiEventComplete)
    {
      mLightConductors = new List<LightConductor>();
      mFanConductors = new List<FanConductor>();
      mRumbleConductors = new List<RumbleConductor>();

      SetupManagers(xiEngine, xiScene, xiEventComplete);
    }

    private void SetupManagers(EngineManager xiEngine, amBXScene xiScene, Action xiAction)
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

    public void RunSynchronised()
    {
      ThreadPool.QueueUserWorkItem(_ => mFrameConductor.Run());
    }

    public void RunDesynchronised()
    {
      mLightConductors.ForEach(light => ThreadPool.QueueUserWorkItem(_ => light.Run()));
      mFanConductors.ForEach(fan => ThreadPool.QueueUserWorkItem(_ => fan.Run()));
      mRumbleConductors.ForEach(rumble => ThreadPool.QueueUserWorkItem(_ => rumble.Run()));
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

    #endregion

    public void EnableSync()
    {
      mFrameConductor.Enable();
      if (!mFrameConductor.IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => mFrameConductor.Run());
      }
    }

    public void DisableSync()
    {
      mFrameConductor.Disable();
    }

    public void EnableDesync()
    {
      mLightConductors.ForEach(light => ThreadPool.QueueUserWorkItem(_ => light.Enable()));
      mFanConductors.ForEach(fan => ThreadPool.QueueUserWorkItem(_ => fan.Enable()));
      mRumbleConductors.ForEach(rumble => ThreadPool.QueueUserWorkItem(_ => rumble.Enable()));
    }

    public void DisableDesync()
    {
      mLightConductors.ForEach(light => ThreadPool.QueueUserWorkItem(_ => light.Disable()));
      mFanConductors.ForEach(fan => ThreadPool.QueueUserWorkItem(_ => fan.Disable()));
      mRumbleConductors.ForEach(rumble => ThreadPool.QueueUserWorkItem(_ => rumble.Disable()));
    }

    //qqUMI This stuff is less than ideal - need to find a way to make this better!
    #region Component-specific implementations

    private void UpdateSceneIfRelevant(LightConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private void EnableAndRunIfRequired(LightConductor xiConductor)
    {
      xiConductor.Enable();
      if (!xiConductor.IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => xiConductor.Run());
      }
    }

    private void UpdateSceneIfRelevant(FanConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private void EnableAndRunIfRequired(FanConductor xiConductor)
    {
      xiConductor.Enable();
      if (!xiConductor.IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => xiConductor.Run());
      }
    }

    private void UpdateSceneIfRelevant(RumbleConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private void EnableAndRunIfRequired(RumbleConductor xiConductor)
    {
      xiConductor.Enable();
      if (!xiConductor.IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => xiConductor.Run());
      }
    }

    #endregion

    private Func<FrameStatistics, eComponentType, eDirection, bool> IsApplicableForConductor =
      (statistics, componentType, direction) => statistics.AreEnabledForComponentAndDirection(componentType, direction);

    private FrameConductor mFrameConductor;
    private List<LightConductor> mLightConductors;
    private List<FanConductor> mFanConductors;
    private List<RumbleConductor> mRumbleConductors;
  }
}

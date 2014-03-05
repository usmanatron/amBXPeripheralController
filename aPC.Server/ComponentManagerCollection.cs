using aPC.Server.EngineActors;
using aPC.Server.Conductors;
using aPC.Common.Server;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using aPC.Server.SceneHandlers;

namespace aPC.Server
{
  class ComponentManagerCollection
  {
    public ComponentManagerCollection(EngineManager xiEngine, amBXScene xiScene, Action xiEventComplete)
    {
      SetupManagers(xiEngine, xiScene, xiEventComplete);
    }

    private void SetupManagers(EngineManager xiEngine, amBXScene xiScene, Action xiAction)
    {
      mLightConductors = new List<LightConductor>();
      mFanConductors = new List<FanConductor>();
      mRumbleConductors = new List<RumbleConductor>();

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

    public void RunAllManagersDeSynchronised(SynchronisationManager xiSyncManager)
    {
      Parallel.ForEach(mLightConductors, conductor => xiSyncManager.RunWhileUnSynchronised(conductor.Run));
      Parallel.ForEach(mFanConductors, conductor => xiSyncManager.RunWhileUnSynchronised(conductor.Run));
      Parallel.ForEach(mRumbleConductors, conductor => xiSyncManager.RunWhileUnSynchronised(conductor.Run));
    }

    public void UpdateAllManagers(amBXScene xiScene)
    {
      Parallel.ForEach(mLightConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
      Parallel.ForEach(mFanConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
      Parallel.ForEach(mRumbleConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
    }

    private void UpdateSceneIfRelevant(LightConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private void UpdateSceneIfRelevant(FanConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private void UpdateSceneIfRelevant(RumbleConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType(), xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private Func<FrameStatistics, eComponentType, eDirection, bool> IsApplicableForConductor =
      (statistics, componentType, direction) => statistics.AreEnabledForComponentAndDirection(componentType, direction);


    private bool RunTaskOnFans(Action<FanConductor> xiAction)
    {
      Parallel.ForEach(mFanConductors, xiAction);
      return true;
    }

    private bool RunTaskOnRumbles(Action<RumbleConductor> xiAction)
    {
      Parallel.ForEach(mRumbleConductors, xiAction);
      return true;
    }

    private List<LightConductor> mLightConductors;
    private List<FanConductor> mFanConductors;
    private List<RumbleConductor> mRumbleConductors;
  }
}

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
    public ComponentManagerCollection(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupManagers(xiEngine, xiEventComplete);
    }

    private void SetupManagers(EngineManager xiEngine, Action xiAction)
    {
      mLightConductors = new List<LightConductor>();
      mFanConductors = new List<FanConductor>();
      mRumbleConductors = new List<RumbleConductor>();

      mLightConductors.Add(new LightConductor(eDirection.North, new LightActor(xiEngine), new LightHandler(eDirection.North, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.NorthEast, new LightActor(xiEngine), new LightHandler(eDirection.NorthEast, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.East, new LightActor(xiEngine), new LightHandler(eDirection.East, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.SouthEast, new LightActor(xiEngine), new LightHandler(eDirection.SouthEast, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.South, new LightActor(xiEngine), new LightHandler(eDirection.South, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.SouthWest, new LightActor(xiEngine), new LightHandler(eDirection.SouthWest, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.West, new LightActor(xiEngine), new LightHandler(eDirection.West, xiAction)));
      mLightConductors.Add(new LightConductor(eDirection.NorthWest, new LightActor(xiEngine), new LightHandler(eDirection.NorthWest, xiAction)));

      mFanConductors.Add(new FanConductor(eDirection.East, new FanActor(xiEngine), new FanHandler(eDirection.East, xiAction)));
      mFanConductors.Add(new FanConductor(eDirection.West, new FanActor(xiEngine), new FanHandler(eDirection.West, xiAction)));

      mRumbleConductors.Add(new RumbleConductor(eDirection.Center, new RumbleActor(xiEngine), new RumbleHandler(eDirection.Center, xiAction)));
    }

    public void RunAllManagersDeSynchronised(SynchronisationManager xiSyncManager)
    {
      Parallel.ForEach(mLightConductors, conductor => xiSyncManager.RunWhileUnSynchronised(conductor.Run));
      Parallel.ForEach(mFanConductors, conductor => xiSyncManager.RunWhileUnSynchronised(manager.Run));
      Parallel.ForEach(mRumbleConductors, conductor => xiSyncManager.RunWhileUnSynchronised(manager.Run));
    }

    public void UpdateAllManagers(amBXScene xiScene)
    {
      Parallel.ForEach(mLightConductors, conductor => UpdateSceneIfRelevant<Light>(conductor, xiScene));
      Parallel.ForEach(mFanConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
      Parallel.ForEach(mRumbleConductors, conductor => UpdateSceneIfRelevant(conductor, xiScene));
    }

    private void UpdateSceneIfRelevant<T>(LightConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType, xiConductor.Direction))
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

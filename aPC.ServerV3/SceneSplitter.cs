using aPC.Common;
using aPC.Common.Entities;
using aPC.ServerV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.ServerV3
{
  public class SceneSplitter
  {
    public RunningDirectionalComponentList runningDirectionalComponentList;

    public SceneSplitter(RunningDirectionalComponentList runningDirectionalComponentsList)
    {
      this.runningDirectionalComponentList = runningDirectionalComponentsList;
    }

    public void SplitScene(amBXScene scene)
    {
      HandleNewScene(scene);
    }

    private void HandleNewScene(amBXScene scene)
    {
      var previousSceneType = runningDirectionalComponentList.SceneType;
      runningDirectionalComponentList.StartUpdate(scene.SceneType);

      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          runningDirectionalComponentList.Clear();
          MergeNewRunningComponentsIntoExisting(scene);
          UpdateRunningComponentForFrame(scene);
          break;
        case eSceneType.Desync:
          MergeNewRunningComponentsIntoExisting(scene);
          break;
        case eSceneType.Event:
          if (previousSceneType == eSceneType.Event)
          {
            throw new InvalidOperationException("You cannot transition from one event to another");
          }
          UpdateRunningComponentForFrame(scene);
          break;
      }
      runningDirectionalComponentList.EndUpdate();
    }

    private void MergeNewRunningComponentsIntoExisting(amBXScene scene)
    {
      foreach (eComponentType componentType in Enum.GetValues(typeof(eComponentType)))
        foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
        {
          if (!scene.FrameStatistics.AreEnabledForComponentAndDirection(new DirectionalComponent(componentType, direction)))
          {
            continue;
          }

          runningDirectionalComponentList.Update(scene, new DirectionalComponent(componentType, direction));
        }
    }

    private void UpdateRunningComponentForFrame(amBXScene scene)
    {
      runningDirectionalComponentList.UpdateSync(scene);
    }
  }
}
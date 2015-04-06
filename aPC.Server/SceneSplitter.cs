using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;

namespace aPC.Server
{
  public class SceneSplitter
  {
    private RunningDirectionalComponentList runningDirectionalComponents;

    public void SplitScene(RunningDirectionalComponentList runningDirectionalComponents, amBXScene scene)
    {
      this.runningDirectionalComponents = runningDirectionalComponents;
      HandleNewScene(scene);
    }

    private void HandleNewScene(amBXScene scene)
    {
      var previousSceneType = runningDirectionalComponents.SceneType;
      runningDirectionalComponents.StartUpdate(scene.SceneType);

      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          runningDirectionalComponents.Clear();
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
      runningDirectionalComponents.EndUpdate();
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

          runningDirectionalComponents.Update(scene, new DirectionalComponent(componentType, direction));
        }
    }

    private void UpdateRunningComponentForFrame(amBXScene scene)
    {
      runningDirectionalComponents.UpdateSync(scene);
    }
  }
}
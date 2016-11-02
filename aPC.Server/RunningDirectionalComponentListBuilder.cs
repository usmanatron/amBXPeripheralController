using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;

namespace aPC.Server
{
  public class RunningDirectionalComponentListBuilder
  {
    public RunningDirectionalComponentList Build(amBXScene scene, eSceneType previousSceneType)
    {
      var componentsList = new RunningDirectionalComponentList();
      componentsList.StartUpdate(scene.SceneType);

      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          componentsList.Clear();
          MergeNewRunningComponentsIntoExisting(scene, componentsList);
          UpdateRunningComponentForFrame(scene, componentsList);
          break;

        case eSceneType.Desync:
          MergeNewRunningComponentsIntoExisting(scene, componentsList);
          break;

        case eSceneType.Event:
          if (previousSceneType == eSceneType.Event)
          {
            throw new InvalidOperationException("You cannot transition from one event to another");
          }
          UpdateRunningComponentForFrame(scene, componentsList);
          break;
      }
      componentsList.EndUpdate();
      return componentsList;
    }

    private void MergeNewRunningComponentsIntoExisting(amBXScene scene, RunningDirectionalComponentList runningDirectionalComponents)
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

    private void UpdateRunningComponentForFrame(amBXScene scene, RunningDirectionalComponentList runningDirectionalComponents)
    {
      runningDirectionalComponents.UpdateSync(scene);
    }
  }
}
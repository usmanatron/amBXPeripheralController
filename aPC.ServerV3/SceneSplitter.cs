using aPC.Common;
using aPC.Common.Entities;
using aPC.ServerV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.ServerV3
{
  internal class SceneSplitter
  {
    public List<RunningDirectionalComponent> RunningComponents;
    public List<DirectionalComponent> UpdatedDirectionalComponents;
    public eSceneType CurrentState;

    public SceneSplitter()
    {
      RunningComponents = new List<RunningDirectionalComponent>();
      UpdatedDirectionalComponents = new List<DirectionalComponent>();
    }

    public void SplitScene(amBXScene scene)
    {
      HandleNewScene(scene);
    }

    private void HandleNewScene(amBXScene scene)
    {
      UpdatedDirectionalComponents.Clear();

      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          RunningComponents.Clear();
          MergeNewRunningComponentsIntoExisting(scene);
          UpdateRunningComponentForFrame(scene);
          break;
        case eSceneType.Desync:
          MergeNewRunningComponentsIntoExisting(scene);
          break;
        case eSceneType.Event:
          if (CurrentState == eSceneType.Event)
          {
            throw new InvalidOperationException("You cannot transition from one event to another");
          }
          UpdateRunningComponentForFrame(scene);
          break;
      }

      CurrentState = scene.SceneType;
    }

    private void MergeNewRunningComponentsIntoExisting(amBXScene scene)
    {
      foreach (eComponentType componentType in Enum.GetValues(typeof(eComponentType)))
        foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
        {
          if (!scene.FrameStatistics.AreEnabledForComponentAndDirection(componentType, direction))
          {
            continue;
          }

          UpdateRunningComponentAndLog(scene, new DirectionalComponent(componentType, direction));
        }
    }

    private void UpdateRunningComponentForFrame(amBXScene scene)
    {
      UpdateRunningComponentAndLog(scene, new DirectionalComponent(null, eDirection.Everywhere));
    }

    private void UpdateRunningComponentAndLog(amBXScene scene, DirectionalComponent directionalComponent)
    {
      var existingComponent = RunningComponents.SingleOrDefault(component => component.DirectionalComponent == directionalComponent);
      if (existingComponent != null)
      {
        RunningComponents.Remove(existingComponent);
      }

      UpdatedDirectionalComponents.Add(directionalComponent);
      RunningComponents.Add(new RunningDirectionalComponent(scene, directionalComponent, new AtypicalFirstRunInfiniteTicker(scene)));
    }
  }
}
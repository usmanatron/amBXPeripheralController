using aPC.Common;
using aPC.Common.Entities;
using aPC.ServerV3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.ServerV3
{
  internal class SceneOrchestrator
  {
    public List<RunningDirectionalComponent> runningComponents;
    public eSceneType currentState;

    public SceneOrchestrator()
    {
      runningComponents = new List<RunningDirectionalComponent>();
    }

    public void UpdateComponents(amBXScene scene)
    {
      HandleNewScene(scene);
    }

    private void HandleNewScene(amBXScene scene)
    {
      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          runningComponents.Clear();
          MergeNewRunningComponentsIntoExisting(scene);
          UpdateRunningComponentForFrame(scene);
          break;
        case eSceneType.Desync:
          MergeNewRunningComponentsIntoExisting(scene);
          break;
        case eSceneType.Event:
          if (currentState == eSceneType.Event)
          {
            throw new InvalidOperationException("You cannot transition from one event to another");
          }
          UpdateRunningComponentForFrame(scene);
          break;
      }

      currentState = scene.SceneType;
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

          UpdateRunningComponent(scene, componentType, direction);
        }
    }

    private void UpdateRunningComponentForFrame(amBXScene scene)
    {
      UpdateRunningComponent(scene, null, eDirection.Everywhere);
    }

    private void UpdateRunningComponent(amBXScene scene, eComponentType? componentType, eDirection direction)
    {
      var existingComponent = runningComponents.SingleOrDefault(component => component.ComponentType == componentType && component.Direction == direction);
      if (existingComponent != null)
      {
        runningComponents.Remove(existingComponent);
      }

      runningComponents.Add(new RunningDirectionalComponent(scene, direction, componentType, new AtypicalFirstRunInfiniteTicker(scene)));
    }
  }
}
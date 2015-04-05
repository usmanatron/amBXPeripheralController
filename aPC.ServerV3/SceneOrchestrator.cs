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
    public List<RunningDirectionalComponent> RunningComponents;
    public List<Tuple<eComponentType?, eDirection>> UpdatedDirectionalComponents;
    public eSceneType CurrentState;

    public SceneOrchestrator()
    {
      RunningComponents = new List<RunningDirectionalComponent>();
      UpdatedDirectionalComponents = new List<Tuple<eComponentType?, eDirection>>();
    }

    public void UpdateComponents(amBXScene scene)
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

          UpdateRunningComponentAndLog(scene, componentType, direction);
        }
    }

    private void UpdateRunningComponentForFrame(amBXScene scene)
    {
      UpdateRunningComponentAndLog(scene, null, eDirection.Everywhere);
    }

    private void UpdateRunningComponentAndLog(amBXScene scene, eComponentType? componentType, eDirection direction)
    {
      var existingComponent = RunningComponents.SingleOrDefault(component => component.ComponentType == componentType && component.Direction == direction);
      if (existingComponent != null)
      {
        RunningComponents.Remove(existingComponent);
      }

      UpdatedDirectionalComponents.Add(new Tuple<eComponentType?, eDirection>(componentType, direction));
      RunningComponents.Add(new RunningDirectionalComponent(scene, direction, componentType, new AtypicalFirstRunInfiniteTicker(scene)));
    }
  }
}
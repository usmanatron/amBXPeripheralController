using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Server.Entities
{
  public class PreRunComponentListBuilder
  {
    public PreRunComponentList Build(amBXScene scene, eSceneType previousSceneType)
    {
      var components = new List<DirectionalComponent>();

      switch (scene.SceneType)
      {
        case eSceneType.Sync:
          components.AddRange(MergeNewRunningComponentsIntoExisting(scene));
          components.Add(AddUpdateForFrame());
          break;

        case eSceneType.Desync:
          components.AddRange(MergeNewRunningComponentsIntoExisting(scene));
          break;

        case eSceneType.Event:
          if (previousSceneType == eSceneType.Event)
          {
            throw new InvalidOperationException("You cannot transition from one event to another");
          }
          components.Add(AddUpdateForFrame());
          break;
      }

      var componentsList = new PreRunComponentList();
      componentsList.Update(scene, components.ToArray());
      return componentsList;
    }

    private IEnumerable<DirectionalComponent> MergeNewRunningComponentsIntoExisting(amBXScene scene)
    {
      foreach (eComponentType componentType in Enum.GetValues(typeof(eComponentType)))
        foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
        {
          if (!scene.FrameStatistics.AreEnabledForComponentAndDirection(new DirectionalComponent(componentType, direction)))
          {
            continue;
          }

          yield return new DirectionalComponent(componentType, direction);
        }
    }

    // Light is ignored by the list Update methods
    private DirectionalComponent AddUpdateForFrame()
    {
      return new DirectionalComponent(eComponentType.Light, eDirection.Everywhere);
    }
  }
}
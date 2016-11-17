using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Server.Entities
{
  public class PreRunComponentListBuilder
  {
    public PreRunComponentList Build(amBXScene scene)
    {
      var components = new List<DirectionalComponent>();

      switch (scene.SceneType)
      {
        case eSceneType.Singular:
          if (scene.HasRepeatableFrames)
          {
            components.AddRange(MergeNewRunningComponentsIntoExisting(scene));
          }
          components.Add(AddUpdateForFrame());
          break;

        case eSceneType.Composite:
          components.AddRange(MergeNewRunningComponentsIntoExisting(scene));
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
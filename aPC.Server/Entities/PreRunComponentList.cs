using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.Server.Entities
{
  public class PreRunComponentList
  {
    private PreRunComponenet frame;
    private readonly List<PreRunComponenet> components;

    public eSceneType SceneType { get; private set; }

    public List<DirectionalComponent> LastUpdatedDirectionalComponents { get; }

    public PreRunComponentList()
    {
      components = new List<PreRunComponenet>();
      LastUpdatedDirectionalComponents = new List<DirectionalComponent>();
    }

    public IEnumerable<PreRunComponenet> Get(eSceneType sceneType)
    {
      switch (sceneType)
      {
        case eSceneType.Singular:
          yield return frame;
          break;
        case eSceneType.Composite:
          foreach (var directionalComponent in LastUpdatedDirectionalComponents)
          {
            yield return components.SingleOrDefault(component => component.DirectionalComponent.Equals(directionalComponent));
          }
          break;
        default:
          throw new ArgumentException($"Unexpected SceneType: {sceneType}");
      }
    }

    public void Update(amBXScene scene, params DirectionalComponent[] directionalComponents)
    {
      SceneType = scene.SceneType;
      LastUpdatedDirectionalComponents.Clear();

      if (scene.SceneType== eSceneType.Singular)
      {
        components.Clear();
      }

      foreach (var component in directionalComponents)
      {
        if (component.Direction == eDirection.Everywhere)
        {
          frame = new PreRunComponenet(scene, new DirectionalComponent(eComponentType.Light, eDirection.Everywhere), new AtypicalFirstRunInfiniteTicker(scene));
          continue;
        }
        var existingComponent = components
          .SingleOrDefault(cmp => cmp.DirectionalComponent.Equals(component));
        if (existingComponent != null)
        {
          components.Remove(existingComponent);
        }

        components.Add(new PreRunComponenet(scene, component, new AtypicalFirstRunInfiniteTicker(scene)));
        LastUpdatedDirectionalComponents.Add(component);
      }
    }
  }
}
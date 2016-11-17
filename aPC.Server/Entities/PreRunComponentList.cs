using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.Entities
{
  public class PreRunComponentList
  {
    private readonly List<IPreRunComponent> preRunComponents;

    public eSceneType SceneType { get; private set; }

    public List<DirectionalComponent> LastUpdatedDirectionalComponents { get; }

    public PreRunComponentList()
    {
      preRunComponents = new List<IPreRunComponent>();
      LastUpdatedDirectionalComponents = new List<DirectionalComponent>();
    }

    public IEnumerable<IPreRunComponent> Get(eSceneType sceneType) //qqUMI Why are we only passing LastUpdated?
    {
      switch (sceneType)
      {
        case eSceneType.Singular:
          yield return preRunComponents.Single();
          break;
        case eSceneType.Composite:
          foreach (var directionalComponent in LastUpdatedDirectionalComponents)
          {
            yield return preRunComponents
              .SingleOrDefault(cmp => ((DirectionalPreRunComponent)cmp).DirectionalComponent.Equals(directionalComponent));
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

      switch (scene.SceneType)
      {
        case eSceneType.Singular:
          UpdateSingular(scene);
          break;
        case eSceneType.Composite:
          UpdateComposite(scene);
          break;
        default:
          throw new ArgumentException("TODO Add Message");
      }
    }

    public void UpdateSingular(amBXScene scene)
    {
      preRunComponents.Clear();
      preRunComponents.Add(new PreRunFrame(scene, new AtypicalFirstRunInfiniteTicker(scene)));
    }

    public void UpdateComposite(amBXScene scene)
    {
      foreach (var component in scene.FrameStatistics.EnabledDirectionalComponents)
      {
        var existingComponent = preRunComponents
          .SingleOrDefault(cmp => ((DirectionalPreRunComponent)cmp).DirectionalComponent.Equals(component));

        if (existingComponent != null)
        {
          preRunComponents.Remove(existingComponent);
        }

        preRunComponents.Add(new DirectionalPreRunComponent(scene, component, new AtypicalFirstRunInfiniteTicker(scene)));
        LastUpdatedDirectionalComponents.Add(component);
      }
    }
  }
}
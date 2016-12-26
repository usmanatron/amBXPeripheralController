using aPC.Common;
using aPC.Common.Entities;
using System;

namespace aPC.Server.Entities
{
  public class ComponentWrapperListBuilder
  {
    private readonly ComponentWrapperList componentsList;

    public ComponentWrapperListBuilder()
    {
      componentsList = new ComponentWrapperList();
    }

    public ComponentWrapperList Build(amBXScene scene)
    {
      switch (scene.SceneType)
      {
        case eSceneType.Composite:
          UpdateComposite(scene);
          break;
        case eSceneType.Singular:
          UpdateSingular(scene);
          break;
        default:
          throw new ArgumentException("qq");
      }

      return componentsList;
    }

    private void UpdateSingular(amBXScene scene)
    {
      componentsList.ReplaceAllWith(new SingularComponentWrapper(scene, new AtypicalFirstRunInfiniteTicker(scene)));
    }

    private void UpdateComposite(amBXScene scene)
    {
      foreach (var component in scene.FrameStatistics.EnabledDirectionalComponents)
      {
        componentsList.MergeComposite(new CompositeComponentWrapper(scene, component, new AtypicalFirstRunInfiniteTicker(scene)));
      }
    }
  }
}
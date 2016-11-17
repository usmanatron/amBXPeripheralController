using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Server.Entities
{
  public class PreRunComponentListBuilder
  {
    public PreRunComponentList Build(amBXScene scene)
    {
      var components = new List<DirectionalComponent>();
      var componentsList = new PreRunComponentList();

      switch (scene.SceneType)
      {
        case eSceneType.Composite:
          componentsList.UpdateComposite(scene);
          break;
        case eSceneType.Singular:
          componentsList.UpdateSingular(scene);
          break;
        default:
          throw new ArgumentException("qq");
      }

      return componentsList;
    }
  }
}
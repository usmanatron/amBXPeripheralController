using aPC.Common.Defaults;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common
{
  public class SceneAccessor
  {
    private readonly DefaultScenes defaultScenes;

    public SceneAccessor(DefaultScenes defaultScenes)
    {
      this.defaultScenes = defaultScenes;
    }

    public amBXScene GetScene(string description)
    {
      var sceneProperty = GetAllIntegratedScenes()
        .SingleOrDefault(property => SceneNameAttribute.MatchesName(property, description));

      if (sceneProperty == null)
      {
        Console.WriteLine("Integrated scene with description {0} not found.", description);
        return null;
      }

      return sceneProperty.GetValue(defaultScenes) as amBXScene;
    }

    public Dictionary<string, amBXScene> GetAllScenes()
    {
      return GetAllIntegratedScenes()
        .ToDictionary(scene => SceneNameAttribute.GetName(scene), scene => (amBXScene)scene.GetValue(defaultScenes));
    }

    private PropertyInfo[] GetAllIntegratedScenes()
    {
      return defaultScenes.GetType()
        .GetProperties();
    }
  }
}
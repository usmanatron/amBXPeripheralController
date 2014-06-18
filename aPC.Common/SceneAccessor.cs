using System;
using System.Linq;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System.Reflection;
using System.Collections.Generic;

namespace aPC.Common
{
  public class SceneAccessor
  {
    public amBXScene GetScene(string xiDescription)
    {
      var lSceneProperty = GetAllIntegratedScenes()
        .SingleOrDefault(property => SceneNameAttribute.MatchesName(property, xiDescription));

      if (lSceneProperty == null)
      {
        Console.WriteLine("Integrated scene with description {0} not found.", xiDescription);
        return null;
      }

      return lSceneProperty.GetValue(mDefaultScenes) as amBXScene;
    }

    public Dictionary<string, amBXScene> GetAllScenes()
    {
      return GetAllIntegratedScenes()
        .ToDictionary(scene => SceneNameAttribute.GetName(scene), scene => (amBXScene)scene.GetValue(mDefaultScenes));
    }

    private PropertyInfo[] GetAllIntegratedScenes()
    {
      return mDefaultScenes
        .GetType()
        .GetProperties();
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}

using System;
using System.Linq;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Common
{
  public class SceneAccessor
  {
    public amBXScene GetScene(string xiDescription)
    {
      var lSceneProperty = mDefaultScenes
        .GetType()
        .GetProperties()
        .SingleOrDefault(property => SceneNameAttribute.MatchesName(property, xiDescription));

      if (lSceneProperty == null)
      {
        Console.WriteLine("Integrated scene with description {0} not found.", xiDescription);
        return null;
      }

      return lSceneProperty.GetValue(mDefaultScenes) as amBXScene;
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}

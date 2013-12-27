using System;
using Common.Defaults;
using Common.Entities;

namespace Common.Accessors
{
  public class SceneAccessor
  {
    public amBXScene GetScene(string xiDescription)
    {
      switch (xiDescription)
      {
        case "CCNet_Red":
          return mDefaultScenes.BuildBroken;
        case "CCNet_Green":
          return mDefaultScenes.BuildSuccess;
        case "CCNet_FlashingYellow":
          return mDefaultScenes.Building;
        case "CCNet_FlashingOrange":
          return mDefaultScenes.BuildBrokenAndBuilding;
        case "CCNet_Grey":
          return mDefaultScenes.BuildNotConnected;

        case "Default_RedVsBlue":
          return mDefaultScenes.DefaultRedVsBlue;
        case "PoolQ2_Event":
          return mDefaultScenes.PoolQ2_Event;

        case "Error_Flash":
          return mDefaultScenes.Error_Flash;
        case "Empty":
          return mDefaultScenes.Empty;

        default:
          Console.WriteLine("Integrated scene with description {0} not found.", xiDescription);
          return null;
      }
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}

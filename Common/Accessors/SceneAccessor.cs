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

        default:
          Console.WriteLine("Integrated scene with description {0} not found - defaulting to off.", xiDescription);
          return mDefaultScenes.LightsOff;
      }
    }

    private static readonly DefaultScenes mDefaultScenes = new DefaultScenes();
  }
}

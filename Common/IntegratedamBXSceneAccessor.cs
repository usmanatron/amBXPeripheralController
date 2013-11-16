using System;
using Common.Entities;

namespace Common
{
  public class IntegratedamBXSceneAccessor
  {

    public amBXScene GetScene(string xiDescription)
    {
      switch (xiDescription)
      {
        case "CCNet_Red":
          return mIntegratedScenes.BuildBroken;
        case "CCNet_Green":
          return mIntegratedScenes.BuildSuccess;
        case "CCNet_FlashingYellow":
          return mIntegratedScenes.Building;
        case "CCNet_FlashingOrange":
          return mIntegratedScenes.BuildBrokenAndBuilding;
        case "CCNet_Grey":
          return mIntegratedScenes.BuildNotConnected;

        case "Default_RedVsBlue":
          return mIntegratedScenes.DefaultRedVsBlue;

        default:
          Console.WriteLine("Integrated scene with description {0} not found - defaulting to off.", xiDescription);
          return mIntegratedScenes.LightsOff;
      }
    }

    public ComponentBase GetComponent(eComponentType xiFrametype, string xiDescription)
    {
      switch (xiFrametype)
      {
        case eComponentType.Light:
          return GetLightFrame(xiDescription);
        case eComponentType.Fan:
          return GetFanFrame(xiDescription);
        case eComponentType.Rumble:
          return GetRumbleFrame(xiDescription);
        default:
          throw new InvalidOperationException("Unexpected Frame type");
      }
    }

    private LightComponent GetLightFrame(string xiDescription)
    {
      switch (xiDescription)
      {
        case "AllOff":
          return IntegratedamBXScenes.LightsOffComponent;
        default:
          throw new InvalidOperationException("Unexpected Light frame type");
      }
    }

    private FanComponent GetFanFrame(string xiDescription)
    {
      throw new NotImplementedException();
    }

    private RumbleComponent GetRumbleFrame(string xiDescription)
    {
      throw new NotImplementedException();
    }


    private static readonly IntegratedamBXScenes mIntegratedScenes = new IntegratedamBXScenes();

  }
}

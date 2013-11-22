using Common.Entities;

namespace Common.Defaults
{
  class DefaultLightComponents
  {
    public static LightComponent Red = new LightComponent
    {
      FadeTime = 500,
      North = DefaultLights.Red,
      NorthEast = DefaultLights.Red,
      East = DefaultLights.Red,
      SouthEast = DefaultLights.Red,
      South = DefaultLights.Red,
      SouthWest = DefaultLights.Red,
      West = DefaultLights.Red,
      NorthWest = DefaultLights.Red,
    };

    public static LightComponent Green = new LightComponent
    {
      FadeTime = 500,
      North = DefaultLights.Green,
      NorthEast = DefaultLights.Green,
      East = DefaultLights.Green,
      SouthEast = DefaultLights.Green,
      South = DefaultLights.Green,
      SouthWest = DefaultLights.Green,
      West = DefaultLights.Green,
      NorthWest = DefaultLights.Green,
    };

    public static LightComponent Yellow = new LightComponent
    {
      FadeTime = 500,
      North = DefaultLights.Yellow,
      NorthEast = DefaultLights.Yellow,
      East = DefaultLights.Yellow,
      SouthEast = DefaultLights.Yellow,
      South = DefaultLights.Yellow,
      SouthWest = DefaultLights.Yellow,
      West = DefaultLights.Yellow,
      NorthWest = DefaultLights.Yellow,
    };

    public static LightComponent Orange = new LightComponent
    {
      FadeTime = 500,
      North = DefaultLights.Orange,
      NorthEast = DefaultLights.Orange,
      East = DefaultLights.Orange,
      SouthEast = DefaultLights.Orange,
      South = DefaultLights.Orange,
      SouthWest = DefaultLights.Orange,
      West = DefaultLights.Orange,
      NorthWest = DefaultLights.Orange,
    };

    public static LightComponent Off = new LightComponent
    {
      FadeTime = 500,
      North = DefaultLights.Off,
      NorthEast = DefaultLights.Off,
      East = DefaultLights.Off,
      SouthEast = DefaultLights.Off,
      South = DefaultLights.Off,
      SouthWest = DefaultLights.Off,
      West = DefaultLights.Off,
      NorthWest = DefaultLights.Off,
    };
  }
}

using amBXLib;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace aPC.Common.Server.Engine
{
  /// <summary>
  ///  Manages the amBXEngine interface - deals with adding and setting stuff etc.
  /// </summary>
  public class EngineManager : IEngine
  {
    private readonly amBX engine;
    private readonly Dictionary<CompassDirection, amBXLight> lights;
    private readonly Dictionary<CompassDirection, amBXFan> fans;
    private readonly Dictionary<CompassDirection, amBXRumble> rumbles;

    public EngineManager()
    {
      engine = new amBX(1, 0, "amBXPeripheralController", "1.0");
      lights = new Dictionary<CompassDirection, amBXLight>();
      fans = new Dictionary<CompassDirection, amBXFan>();
      rumbles = new Dictionary<CompassDirection, amBXRumble>();
      InitialiseEngine();
    }

    #region Engine Setup

    private void InitialiseEngine()
    {
      CreateLight(CompassDirection.North);
      CreateLight(CompassDirection.NorthEast);
      CreateLight(CompassDirection.East);
      CreateLight(CompassDirection.SouthEast);
      CreateLight(CompassDirection.South);
      CreateLight(CompassDirection.SouthWest);
      CreateLight(CompassDirection.West);
      CreateLight(CompassDirection.NorthWest);

      CreateFan(CompassDirection.East);
      CreateFan(CompassDirection.West);

      CreateRumble(CompassDirection.Center);
    }

    private void CreateLight(CompassDirection direction)
    {
      var light = engine.CreateLight(direction, RelativeHeight.AnyHeight);
      lights.Add(direction, light);
    }

    private void CreateFan(CompassDirection direction)
    {
      var fan = engine.CreateFan(direction, RelativeHeight.AnyHeight);
      fans.Add(direction, fan);
    }

    private void CreateRumble(CompassDirection direction)
    {
      var rumble = engine.CreateRumble(direction, RelativeHeight.AnyHeight);
      rumbles.Add(direction, rumble);
    }

    #endregion Engine Setup

    #region Updating

    public void UpdateComponent(DirectionalComponent component)
    {
      if (component == null)
      {
        return;
      }

      var convertedDirection = ConversionHelpers.GetDirection(component.Direction);
      switch (component.ComponentType)
      {
        case eComponentType.Light:
          ThreadPool.QueueUserWorkItem(_ => UpdateLightInternal(lights[convertedDirection], (Light)component));
          break;
        case eComponentType.Fan:
          UpdateFanInternal(fans[convertedDirection], (Fan)component);
          break;
        case eComponentType.Rumble:
          UpdateRumbleInternal(rumbles[convertedDirection], (Rumble)component);
          break;
      }
    }

    private void UpdateLightInternal(amBXLight light, Light inputLight)
    {
      light.Color = new amBXColor { Red = inputLight.Red, Green = inputLight.Green, Blue = inputLight.Blue };
      light.FadeTime = inputLight.FadeTime;
    }

    private void UpdateFanInternal(amBXFan fan, Fan inputFan)
    {
      fan.Intensity = inputFan.Intensity;
    }

    private void UpdateRumbleInternal(amBXRumble rumble, Rumble inputRumble)
    {
      RumbleType rumbleType;

      try
      {
        rumbleType = ConversionHelpers.GetRumbleType(inputRumble.RumbleType);
      }
      catch (InvalidOperationException)
      {
        return;
      }

      rumble.RumbleSetting = new amBXRumbleSetting
      {
        Intensity = inputRumble.Intensity,
        Speed = inputRumble.Speed,
        Type = rumbleType
      };
    }

    #endregion Updating

    public void Dispose()
    {
      engine.Dispose();
    }
  }
}
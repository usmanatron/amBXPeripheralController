using amBXLib;
using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace aPC.ServerV3.Engine
{
  /// <summary>
  ///  Manages the amBXEngine interface.
  /// </summary>
  internal class AmbxEngineWrapper : IDisposable
  {
    private readonly amBX engine;
    private readonly Dictionary<eDirection, amBXLight> lights;
    private readonly Dictionary<eDirection, amBXFan> fans;
    private readonly Dictionary<eDirection, amBXRumble> rumbles;

    public AmbxEngineWrapper()
    {
      engine = new amBX(1, 0, "amBXPeripheralController", "3.0");
      lights = new Dictionary<eDirection, amBXLight>();
      fans = new Dictionary<eDirection, amBXFan>();
      rumbles = new Dictionary<eDirection, amBXRumble>();
      InitialiseEngine();
    }

    #region Engine Setup

    private void InitialiseEngine()
    {
      foreach (CompassDirection compassDirection in Enum.GetValues(typeof(CompassDirection)))
      {
        CreateLight(compassDirection);
      }
      CreateFan(CompassDirection.East);
      CreateFan(CompassDirection.West);
      CreateRumble(CompassDirection.Center);
    }

    private void CreateLight(CompassDirection direction)
    {
      var light = engine.CreateLight(direction, RelativeHeight.AnyHeight);
      lights.Add((eDirection)direction, light);
    }

    private void CreateFan(CompassDirection direction)
    {
      var fan = engine.CreateFan(direction, RelativeHeight.AnyHeight);
      fans.Add((eDirection)direction, fan);
    }

    private void CreateRumble(CompassDirection direction)
    {
      var rumble = engine.CreateRumble(direction, RelativeHeight.AnyHeight);
      rumbles.Add((eDirection)direction, rumble);
    }

    #endregion Engine Setup

    #region Engine Updates

    public void UpdateLight(Light inputLight)
    {
      var light = lights[inputLight.Direction];
      light.Color = new amBXColor { Red = inputLight.Red, Green = inputLight.Green, Blue = inputLight.Blue };
      light.FadeTime = inputLight.FadeTime;
    }

    public void UpdateFan(Fan inputFan)
    {
      var fan = fans[inputFan.Direction];
      fan.Intensity = inputFan.Intensity;
    }

    public void UpdateRumble(Rumble inputRumble)
    {
      var rumble = rumbles[inputRumble.Direction];

      rumble.RumbleSetting = new amBXRumbleSetting
      {
        Intensity = inputRumble.Intensity,
        Speed = inputRumble.Speed,
        Type = (RumbleType)inputRumble.RumbleType
      };
    }

    #endregion Engine Updates

    public void Dispose()
    {
      engine.Dispose();
    }
  }
}
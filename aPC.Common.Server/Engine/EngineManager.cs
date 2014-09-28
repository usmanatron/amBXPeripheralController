using amBXLib;
using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Threading;

namespace aPC.Common.Server.Engine
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  public class EngineManager : IEngine
  {
    public EngineManager()
    {
      mEngine = new amBX(1, 0, "amBXPeripheralController", "1.0");
      mLights = new Dictionary<CompassDirection, amBXLight>();
      mFans = new Dictionary<CompassDirection, amBXFan>();
      mRumbles = new Dictionary<CompassDirection, amBXRumble>();
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

    private void CreateLight(CompassDirection xiDirection)
    {
      var lLight = mEngine.CreateLight(xiDirection, RelativeHeight.AnyHeight);
      mLights.Add(xiDirection, lLight);
    }

    private void CreateFan(CompassDirection xiDirection)
    {
      var lFan = mEngine.CreateFan(xiDirection, RelativeHeight.AnyHeight);
      mFans.Add(xiDirection, lFan);
    }

    private void CreateRumble(CompassDirection xiDirection)
    {
      var lRumble = mEngine.CreateRumble(xiDirection, RelativeHeight.AnyHeight);
      mRumbles.Add(xiDirection, lRumble);
    }

    #endregion

    #region Updating

    public void UpdateLight(eDirection xiDirection, Light xiLight, int xiFadeTime)
    {
      var lDirection = ConversionHelpers.GetDirection(xiDirection);
      ThreadPool.QueueUserWorkItem(_ => UpdateLightInternal(mLights[lDirection], xiLight, xiFadeTime));
    }

    private void UpdateLightInternal(amBXLight xiLight, Light xiInputLight, int xiFadeTime)
    {
      if (xiInputLight == null)
      {
        // No change - don't touch!
        return;
      }
      xiLight.Color = new amBXColor { Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue };
      xiLight.FadeTime = xiFadeTime;
    }

    public void UpdateFan(eDirection xiDirection, Fan xiFan)
    {
      var lDirection = ConversionHelpers.GetDirection(xiDirection);
      UpdateFanInternal(mFans[lDirection], xiFan);
    }

    private void UpdateFanInternal(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    public void UpdateRumble(eDirection xiDirection, Rumble xiRumble)
    {
      var lDirection = ConversionHelpers.GetDirection(xiDirection);
      UpdateRumbleInternal(mRumbles[lDirection], xiRumble);
    }

    private void UpdateRumbleInternal(amBXRumble xiRumble, Rumble xiInputRumble)
    {
      if (xiInputRumble == null)
      {
        return;
      }

      RumbleType lRumbleType;

      try
      {
        lRumbleType = ConversionHelpers.GetRumbleType(xiInputRumble.RumbleType);
      }
      catch (InvalidOperationException)
      {
        return;
      }

      xiRumble.RumbleSetting = new amBXRumbleSetting
      {
        Intensity = xiInputRumble.Intensity,
        Speed = xiInputRumble.Speed,
        Type = lRumbleType
      };
    }

    #endregion

    public void Dispose()
    {
      mEngine.Dispose();
    }

    private readonly amBX mEngine;

    private readonly Dictionary<CompassDirection, amBXLight> mLights;
    private readonly Dictionary<CompassDirection, amBXFan> mFans;
    private readonly Dictionary<CompassDirection, amBXRumble> mRumbles;
  }
}

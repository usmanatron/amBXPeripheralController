using Common.Entities;
using Common.Integration;
using System;
using amBXLib;
using System.Collections;
using System.Collections.Generic;

namespace Common.Server.Managers
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  public abstract class EngineManagerBase : IDisposable
  {
    protected EngineManagerBase()
    {
      mEngine = new amBX(1, 0, "amBXNotification", "1.0");
      mLights = new Dictionary<CompassDirection, amBXLight>();
      mFans = new Dictionary<CompassDirection, amBXFan>();
      mRumbles = new Dictionary<CompassDirection, amBXRumble>();
      InitialiseEngine(mEngine);
    }

    private void InitialiseEngine(amBX xiEngine)
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

      CreateRumble(CompassDirection.Everywhere);
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

    #region Updating

    protected void UpdateLight(CompassDirection xiDirection, Light xiInputLight, int xiFadeTime)
    {
      UpdateLightInternal(mLights[xiDirection], xiInputLight, xiFadeTime);
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

    protected void UpdateFan(CompassDirection xiDirection, Fan xiInputLight)
    {
      UpdateFanInternal(mFans[xiDirection], xiInputLight);
    }

    private void UpdateFanInternal(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    protected void UpdateRumble(CompassDirection xiDirection, RumbleComponent xiInputRumble)
    {
      UpdateRumbleInternal(mRumbles[xiDirection], xiInputRumble);
    }

    protected void UpdateRumbleInternal(amBXRumble xiRumble, RumbleComponent xiInputRumble)
    {
      if (xiInputRumble == null)
      {
        return;
      }

      RumbleType lRumbleType;

      try
      {
        lRumbleType = RumbleTypeConverter.GetRumbleType(xiInputRumble.RumbleType);
      }
      catch (InvalidRumbleException)
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

    private Dictionary<CompassDirection, amBXLight> mLights;
    private Dictionary<CompassDirection, amBXFan> mFans;
    private Dictionary<CompassDirection, amBXRumble> mRumbles;
  }
}

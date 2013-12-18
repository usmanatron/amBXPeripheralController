using System;
using amBXLib;
using Common.Entities;
using Common.Integration;

namespace Server
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  class EngineManager : IDisposable
  {
    public EngineManager()
    {
      mEngine = new amBX(1, 0, "amBXNotification", "1.0");
      InitialiseEngine(mEngine);
    }

    private void InitialiseEngine(amBX xiEngine)
    {
      mNorthLight = xiEngine.CreateLight(CompassDirection.North, RelativeHeight.AnyHeight);
      mNorthEastLight = xiEngine.CreateLight(CompassDirection.NorthEast, RelativeHeight.AnyHeight);
      mEastLight = xiEngine.CreateLight(CompassDirection.East, RelativeHeight.AnyHeight);
      mSouthEastLight = xiEngine.CreateLight(CompassDirection.SouthEast, RelativeHeight.AnyHeight);
      mSouthLight = xiEngine.CreateLight(CompassDirection.South, RelativeHeight.AnyHeight);
      mSouthWestLight = xiEngine.CreateLight(CompassDirection.SouthWest, RelativeHeight.AnyHeight);
      mWestLight = xiEngine.CreateLight(CompassDirection.West, RelativeHeight.AnyHeight);
      mNorthWestLight = xiEngine.CreateLight(CompassDirection.NorthWest, RelativeHeight.AnyHeight);

      mEastFan = xiEngine.CreateFan(CompassDirection.East, RelativeHeight.AnyHeight);
      mWestFan = xiEngine.CreateFan(CompassDirection.West, RelativeHeight.AnyHeight);

      mRumble = xiEngine.CreateRumble(CompassDirection.Everywhere, RelativeHeight.AnyHeight);
    }


    internal void UpdateLights(LightComponent xiLights)
    {
      UpdateLight(mNorthLight, xiLights.North, xiLights.FadeTime);
      UpdateLight(mNorthEastLight, xiLights.NorthEast, xiLights.FadeTime);
      UpdateLight(mEastLight, xiLights.East, xiLights.FadeTime);
      UpdateLight(mSouthEastLight, xiLights.SouthEast, xiLights.FadeTime);
      UpdateLight(mSouthLight, xiLights.South, xiLights.FadeTime);
      UpdateLight(mSouthWestLight, xiLights.SouthWest, xiLights.FadeTime);
      UpdateLight(mWestLight, xiLights.West, xiLights.FadeTime);
      UpdateLight(mNorthWestLight, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateLight(amBXLight xiLight, Light xiInputLight, int xiFadeTime)
    {
      if (xiInputLight == null)
      {
        // No change - don't touch!
        return;
      }
      xiLight.Color = new amBXColor { Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue };
      xiLight.FadeTime = xiFadeTime;
    }

    internal void UpdateFans(FanComponent xiFans)
    {
      UpdateFan(mEastFan, xiFans.East);
      UpdateFan(mWestFan, xiFans.West);
    }

    private void UpdateFan(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    internal void UpdateRumbles(RumbleComponent xiInputRumble)
    {
      UpdateRumble(mRumble, xiInputRumble);
    }

    private void UpdateRumble(amBXRumble xiRumble, RumbleComponent xiInputRumble)
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

    public void Dispose()
    {
      mEngine.Dispose();
    }

    private readonly amBX mEngine;

    #region amBXLib Members

    #region Lights

    private amBXLight mNorthLight;
    private amBXLight mNorthEastLight;
    private amBXLight mEastLight;
    private amBXLight mSouthEastLight;
    private amBXLight mSouthLight;
    private amBXLight mSouthWestLight;
    private amBXLight mWestLight;
    private amBXLight mNorthWestLight;

    #endregion

    #region Fans

    private amBXFan mEastFan;
    private amBXFan mWestFan;

    #endregion

    private amBXRumble mRumble;

    #endregion
  }
}

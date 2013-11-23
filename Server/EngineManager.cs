using System;
using amBXLib;
using Common.Accessors;
using Common.Entities;

namespace Server
{
  // Manages the amBXEngine interface - deals with adding and setting stuff etc.
  class EngineManager : IDisposable
  {
    public EngineManager()
    {
      mComponentAccessor = new ComponentAccessor();
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
      //qqUMI Remove this null check by adding the AllOff stuff into the null case of GetNextframe()
      if (xiLights == null)
      {
        // this can only happen if the set of Light Frames only contains non-repeatable frames
        // and we've used them all up.  
        // In this case (and this case only!) we want to switch all lights off
        xiLights = (LightComponent)mComponentAccessor.GetComponent(eComponentType.Light, "AllOff");
      }

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
      if (xiFans == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        xiFans = (FanComponent)mComponentAccessor.GetComponent(eComponentType.Fan, "AllOff"); //qqUMI Will throw
      }

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

    public void UpdateRumble(RumbleComponent xiRumble)
    {
      if (xiRumble == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        xiRumble = (RumbleComponent)mComponentAccessor.GetComponent(eComponentType.Rumble, "AllOff"); //qqUMI Will throw
      }

      //qqUMI Rumble not supported yet
    }

    public void Dispose()
    {
      mEngine.Dispose();
    }

    private readonly ComponentAccessor mComponentAccessor;
    private amBX mEngine;

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

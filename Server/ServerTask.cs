using System.Threading;
using amBXLib;
using Common;
using Common.Entities;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public ServerTask()
    {
      MSMQAdministrator.SetupQueueAccess();
      mDefaultsAccessor = new IntegratedamBXSceneAccessor();
      mManager = new amBXSceneManager(mDefaultsAccessor.GetScene("Default_RedVsBlue"));
    }

    public void Run()
    {
      using (var lEngine = new amBX(1, 0, "amBXNotificationServer", "1.0"))
      {
        InitialiseEngine(lEngine);

        while (true)
        {
          GetNewScene();
          ApplyNextFrame();
          WaitforInterval();
        }
      }
    }

    private void InitialiseEngine(amBX xiEngine)
    {
      // Lights
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

    private void GetNewScene()
    {
      var lScene = new MessageParser().GetNewScene();

      if (lScene != null)
      {
        // New Scene!
        mManager = new amBXSceneManager(lScene);
      }
    }

    private void ApplyNextFrame()
    {
      if (mManager.IsLightEnabled)
      {
        UpdateLights();
      }

      if (mManager.IsFanEnabled)
      {
        UpdateFans();
      }

      if (mManager.IsRumbleEnabled)
      {
        UpdateRumble();
      }
    }

    private void UpdateLights()
    {
      var lLightFrame = mManager.GetNextLightFrame();
      if (lLightFrame == null)
      {
        // this can only happen if the set of Light Frames only contains non-repeatable frames
        // and we've used them all up.  
        // In this case (and this case only!) we want to switch all lights off
        lLightFrame = (LightFrame) mDefaultsAccessor.GetFrame(eFrameType.Light, "AllOff");
      }

      UpdateLight(mNorthLight, lLightFrame.North, lLightFrame.FadeTime);
      UpdateLight(mNorthEastLight, lLightFrame.NorthEast, lLightFrame.FadeTime);
      UpdateLight(mEastLight, lLightFrame.East, lLightFrame.FadeTime);
      UpdateLight(mSouthEastLight, lLightFrame.SouthEast, lLightFrame.FadeTime);
      UpdateLight(mSouthLight, lLightFrame.South, lLightFrame.FadeTime);
      UpdateLight(mSouthWestLight, lLightFrame.SouthWest, lLightFrame.FadeTime);
      UpdateLight(mWestLight, lLightFrame.West, lLightFrame.FadeTime);
      UpdateLight(mNorthWestLight, lLightFrame.NorthWest, lLightFrame.FadeTime);
    }

    private void UpdateLight(amBXLight xiLight, Light xiInputLight, int xiFadeTime)
    {
      if (xiInputLight == null)
      {
        // No change - don't touch!
        return;
      }

      xiLight.Color = new amBXColor{Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue};
      xiLight.FadeTime = xiFadeTime;
    }

    private void UpdateFans()
    {
      var lFanFrame = mManager.GetNextFanFrame();
      if (lFanFrame == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        return;
      }

      ApplyChangeToFan(mEastFan, lFanFrame.East);
      ApplyChangeToFan(mWestFan, lFanFrame.West);
    }

    private void ApplyChangeToFan(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    private void UpdateRumble()
    {
      var lFrame = mManager.GetNextRumbleFrame();
      if (lFrame == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        return;
      }
      
      //qqUMI Rumble not supported yet
    }

    private void WaitforInterval()
    {
      Thread.Sleep(mManager.FrameLength);
    }

    private amBXSceneManager mManager;
    private IntegratedamBXSceneAccessor mDefaultsAccessor;

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

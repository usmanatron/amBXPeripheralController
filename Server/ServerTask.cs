using System.ServiceModel;
using System.Threading;
using amBXLib;
using Common.Accessors;
using Common.Communication;
using Common.Entities;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public ServerTask()
    {
      SetupAccess();
      mComponentAccessor = new ComponentAccessor();
      
      var lSceneAccessor = new SceneAccessor();
      Manager = new amBXSceneManager(lSceneAccessor.GetScene("Default_RedVsBlue"));
    }

    private void SetupAccess()
    {
      mHost = new ServiceHost(typeof (NotificationService));
      mHost.AddServiceEndpoint(typeof (INotificationService), 
                               new BasicHttpBinding(),
                               CommunicationSettings.ServiceUrl);
      mHost.Open();
    }

    public void Run()
    {
      using (var lEngine = new amBX(1, 0, "amBXNotificationServer", "1.0"))
      {
        InitialiseEngine(lEngine);

        while (true)
        {
          lock (Manager)
          {
            ActNextFrame();
            AdvanceScene();
          }
        }
      }
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

    private void ActNextFrame()
    {
      var lFrame = Manager.GetNextFrame();

      if (Manager.IsLightEnabled)
      {
        UpdateLights(lFrame.Lights);
      }

      if (Manager.IsFanEnabled)
      {
        UpdateFans(lFrame.Fans);
      }

      if (Manager.IsRumbleEnabled)
      {
        UpdateRumble(lFrame.Rumble);
      }

      WaitforInterval(lFrame.Length);
    }

    private void UpdateLights(LightComponent xiLights)
    {
      //qqUMI Remove this null check by adding the AllOff stuff into the null case of GetNextframe()
      if (xiLights == null)
      {
        // this can only happen if the set of Light Frames only contains non-repeatable frames
        // and we've used them all up.  
        // In this case (and this case only!) we want to switch all lights off
        xiLights = (LightComponent) mComponentAccessor.GetComponent(eComponentType.Light, "AllOff");
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

      xiLight.Color = new amBXColor{Red = xiInputLight.Red, Green = xiInputLight.Green, Blue = xiInputLight.Blue};
      xiLight.FadeTime = xiFadeTime;
    }

    private void UpdateFans(FanComponent xiFans)
    {
      if (xiFans == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        xiFans = (FanComponent)mComponentAccessor.GetComponent(eComponentType.Fan, "AllOff"); //qqUMI Will throw
      }

      ApplyChangeToFan(mEastFan, xiFans.East);
      ApplyChangeToFan(mWestFan, xiFans.West);
    }

    private void ApplyChangeToFan(amBXFan xiFan, Fan xiInputFan)
    {
      if (xiInputFan == null)
      {
        return;
      }
      xiFan.Intensity = xiInputFan.Intensity;
    }

    private void UpdateRumble(RumbleComponent xiRumble)
    {
      if (xiRumble == null)
      {
        // qqUMI -  See the Light equivalent and finish properly!
        xiRumble = (RumbleComponent)mComponentAccessor.GetComponent(eComponentType.Rumble, "AllOff"); //qqUMI Will throw
      }
      
      //qqUMI Rumble not supported yet
    }

    private void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    private void AdvanceScene()
    {
      Manager.AdvanceScene();
    }

    public static amBXSceneManager Manager;
    private readonly ComponentAccessor mComponentAccessor;
    private ServiceHost mHost;

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

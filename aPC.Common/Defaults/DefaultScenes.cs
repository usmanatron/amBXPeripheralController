using System.Collections.Generic;
using aPC.Common.Entities;

namespace aPC.Common.Defaults
{
  class DefaultScenes
  {
    #region CruiseControl.NET Defaults

    public amBXScene BuildBroken
    {
      get
      {
        var lScene = BasicScene;
        var lFrame = BasicFrame;
        lFrame.Lights = DefaultLightComponents.Red;
        lScene.Frames = new List<Frame> { lFrame };
        return lScene;
      }
    }

    public amBXScene BuildSuccess
    {
      get
      {
        var lScene = BasicScene;
        var lFrame = BasicFrame;
        lFrame.Lights = DefaultLightComponents.Green;
        lScene.Frames = new List<Frame> { lFrame };
        return lScene;
      }
    }

    public amBXScene Building
    {
      get
      {
        var lScene = BasicScene;
        var lFrameOn = BasicFrame;
        var lFrameOff = BasicFrame;
        lFrameOn.Lights = DefaultLightComponents.Yellow;
        lFrameOff.Lights = DefaultLightComponents.Off;

        lScene.Frames = new List<Frame> { lFrameOn, lFrameOff };
        return lScene;
      }
    }

    public amBXScene BuildBrokenAndBuilding
    {
      get
      {
        var lScene = BasicScene;
        var lFrameOn = BasicFrame;
        var lFrameOff = BasicFrame;
        lFrameOn.Lights = DefaultLightComponents.Orange;
        lFrameOff.Lights = DefaultLightComponents.Off;

        lScene.Frames = new List<Frame> { lFrameOn, lFrameOff };
        return lScene;
      }
    }

    public amBXScene BuildNotConnected
    {
      get
      {
        var lScene = BasicScene;
        var lFrame = BasicFrame;
        lFrame.Lights = DefaultLightComponents.Off;

        lScene.Frames = new List<Frame> { lFrame };
        return lScene;
      }
    }

    #endregion

    #region Misc

    public amBXScene LightsOff
    {
      get
      {
        return BuildNotConnected;
      }
    }

    public amBXScene DefaultRedVsBlue
    {
      get
      {
        var lScene = BasicScene;
        lScene.IsSynchronised = true;
        var lPurple = BasicFrame;
        var lDisableFans = BasicFrame;
        var lRedBlue = BasicFrame;
        var lBlueRed = BasicFrame;

        lPurple.IsRepeated = false;
        lPurple.Length = 2000;
        lPurple.Lights = new LightComponent
                         {
                           FadeTime = 100,
                           West = DefaultLights.SoftPurple,
                           NorthWest = DefaultLights.SoftPurple,
                           North = DefaultLights.SoftPurple,
                           NorthEast = DefaultLights.SoftPurple,
                           East = DefaultLights.SoftPurple
                         };
        lPurple.Fans = new FanComponent
                       {
                         East = DefaultFans.FullPower,
                         West = DefaultFans.FullPower
                       };

        lDisableFans.IsRepeated = false;
        lDisableFans.Length = 10;
        lDisableFans.Fans = new FanComponent
                            {
                              East = DefaultFans.Off,
                              West = DefaultFans.Off
                            };

        lRedBlue.Lights = new LightComponent
                          {
                            FadeTime = 100,
                            West = DefaultLights.Red,
                            NorthWest = DefaultLights.Red,
                            North = DefaultLights.SoftPurple,
                            NorthEast = DefaultLights.Blue,
                            East = DefaultLights.Blue
                          };
        lBlueRed.Lights = new LightComponent
                          {
                            FadeTime = 100,
                            West = DefaultLights.Blue,
                            NorthWest = DefaultLights.Blue,
                            North = DefaultLights.SoftPurple,
                            NorthEast = DefaultLights.Red,
                            East = DefaultLights.Red
                          };

        lScene.Frames = new List<Frame> {lPurple, lDisableFans, lRedBlue, lBlueRed};
        return lScene;
      }
    }

    public amBXScene Empty
    {
      get
      {
        var lScene = BasicScene;
        lScene.Frames = new List<Frame>();
        return lScene;
      }
    }

    public amBXScene Error_Flash
    {
      get
      {
        var lScene = BasicScene;
        lScene.IsEvent = true;
        lScene.IsSynchronised = true;

        var lOff = BasicFrame;
        lOff.Length = 100;
        lOff.Lights = DefaultLightComponents.Off;
        lOff.Lights.FadeTime = 10;

        var lError = BasicFrame;
        lError.Length = 200;
        lError.Lights = DefaultLightComponents.Red;
        lError.Lights.FadeTime = 10;

        lScene.Frames = new List<Frame> { lOff, lError, lOff, lError, lOff };
        return lScene;
      }
    }

    #endregion

    #region PoolQ2

    public amBXScene PoolQ2_Event
    {
      get
      {
        var lScene = BasicScene;
        lScene.IsEvent = true;
        lScene.IsExclusive = true;
        lScene.IsSynchronised = true;

        var lPrePink  = BasicFrame;
        lPrePink.IsRepeated = false;
        lPrePink.Length = 200;
        lPrePink.Lights = new LightComponent
        {
          FadeTime = 100,
          West = DefaultLights.SoftPink,
          NorthWest = DefaultLights.SoftPink,
          North = DefaultLights.SoftPink,
          NorthEast = DefaultLights.SoftPink,
          East = DefaultLights.SoftPink
        };
        lPrePink.Rumble = DefaultRumbleComponents.Off;

        var lPurple = BasicFrame;
        lPurple.IsRepeated = false;
        lPurple.Length = 1000;
        lPurple.Lights = new LightComponent
        {
          FadeTime = 200,
          West = DefaultLights.StrongPurple,
          NorthWest = DefaultLights.StrongPurple,
          North = DefaultLights.StrongPurple,
          NorthEast = DefaultLights.StrongPurple,
          East = DefaultLights.StrongPurple
        };
        lPurple.Fans = new FanComponent
        {
          East = DefaultFans.FullPower,
          West = DefaultFans.FullPower
        };
        lPurple.Rumble = DefaultRumbleComponents.Thunder;


        var lPostPink = BasicFrame;
        lPostPink.IsRepeated = false;
        lPostPink.Length = 500;
        lPostPink.Lights = new LightComponent
        {
          FadeTime = 100,
          West = DefaultLights.SoftPink,
          NorthWest = DefaultLights.SoftPink,
          North = DefaultLights.SoftPink,
          NorthEast = DefaultLights.SoftPink,
          East = DefaultLights.SoftPink
        };
        lPostPink.Fans = new FanComponent
        {
          East = DefaultFans.Off,
          West = DefaultFans.Off
        };
        lPostPink.Rumble = DefaultRumbleComponents.Off;

        lScene.Frames = new List<Frame> {lPrePink, lPurple, lPostPink};
        return lScene;
      }
    }

    #endregion

    #region Helpers

    private amBXScene BasicScene
    {
      get
      {
        return new amBXScene
        {
          IsExclusive = true,
          Frames = new List<Frame>()
        };
      }
    }

    private Frame BasicFrame
    {
      get
      {
        return new Frame
        {
          IsRepeated = true,
          Length = 1000
        };
      }
    }

    #endregion
  }
}

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
        lFrame.Lights = DefaultLightSections.Red;
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
        lFrame.Lights = DefaultLightSections.Green;
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
        lFrameOn.Lights = DefaultLightSections.Yellow;
        lFrameOff.Lights = DefaultLightSections.Off;

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
        lFrameOn.Lights = DefaultLightSections.Orange;
        lFrameOff.Lights = DefaultLightSections.Off;

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
        lFrame.Lights = DefaultLightSections.Off;

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
        lPurple.Lights = new LightSection
                         {
                           FadeTime = 100,
                           West = DefaultLights.SoftPurple,
                           NorthWest = DefaultLights.SoftPurple,
                           North = DefaultLights.SoftPurple,
                           NorthEast = DefaultLights.SoftPurple,
                           East = DefaultLights.SoftPurple
                         };
        lPurple.Fans = new FanSection
                       {
                         East = DefaultFans.FullPower,
                         West = DefaultFans.FullPower
                       };

        lDisableFans.IsRepeated = false;
        lDisableFans.Length = 10;
        lDisableFans.Fans = new FanSection
                            {
                              East = DefaultFans.Off,
                              West = DefaultFans.Off
                            };

        lRedBlue.Lights = new LightSection
                          {
                            FadeTime = 100,
                            West = DefaultLights.Red,
                            NorthWest = DefaultLights.Red,
                            North = DefaultLights.SoftPurple,
                            NorthEast = DefaultLights.Blue,
                            East = DefaultLights.Blue
                          };
        lBlueRed.Lights = new LightSection
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
        lOff.Lights = DefaultLightSections.Off;
        lOff.Lights.FadeTime = 10;

        var lError = BasicFrame;
        lError.Length = 200;
        lError.Lights = DefaultLightSections.Red;
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
        lPrePink.Lights = new LightSection
        {
          FadeTime = 100,
          West = DefaultLights.SoftPink,
          NorthWest = DefaultLights.SoftPink,
          North = DefaultLights.SoftPink,
          NorthEast = DefaultLights.SoftPink,
          East = DefaultLights.SoftPink
        };
        lPrePink.Rumbles = DefaultRumbleSections.Off;

        var lPurple = BasicFrame;
        lPurple.IsRepeated = false;
        lPurple.Length = 1000;
        lPurple.Lights = new LightSection
        {
          FadeTime = 200,
          West = DefaultLights.StrongPurple,
          NorthWest = DefaultLights.StrongPurple,
          North = DefaultLights.StrongPurple,
          NorthEast = DefaultLights.StrongPurple,
          East = DefaultLights.StrongPurple
        };
        lPurple.Fans = DefaultFanSections.Full;
        lPurple.Rumbles = DefaultRumbleSections.Thunder;


        var lPostPink = BasicFrame;
        lPostPink.IsRepeated = false;
        lPostPink.Length = 500;
        lPostPink.Lights = new LightSection
        {
          FadeTime = 100,
          West = DefaultLights.SoftPink,
          NorthWest = DefaultLights.SoftPink,
          North = DefaultLights.SoftPink,
          NorthEast = DefaultLights.SoftPink,
          East = DefaultLights.SoftPink
        };
        lPostPink.Fans = DefaultFanSections.Off;
        lPostPink.Rumbles = DefaultRumbleSections.Off;

        lScene.Frames = new List<Frame> {lPrePink, lPurple, lPostPink};
        return lScene;
      }
    }

    #endregion

    #region Shiprec

    public amBXScene Shiprec_Praise
    {
      get
      {
        var lScene = BasicScene;
        lScene.IsEvent = true;
        lScene.IsExclusive = true;
        lScene.IsSynchronised = true;

        var lPreYellow  = BasicFrame;
        lPreYellow.IsRepeated = false;
        lPreYellow.Length = 200;
        lPreYellow.Lights = new LightSection
        {
          FadeTime = 100,
          West = DefaultLights.SoftYellow,
          NorthWest = DefaultLights.SoftYellow,
          North = DefaultLights.SoftYellow,
          NorthEast = DefaultLights.SoftYellow,
          East = DefaultLights.SoftYellow
        };
        lPreYellow.Rumbles = DefaultRumbleSections.Off;
        lPreYellow.Fans = DefaultFanSections.Off;

        var lYellowTransition = BasicFrame;
        lYellowTransition.IsRepeated = false;
        lYellowTransition.Length = 500;
        lYellowTransition.Lights = new LightSection
        {
          FadeTime = 500,
          West = DefaultLights.Yellow,
          NorthWest = DefaultLights.Yellow,
          North = DefaultLights.Yellow,
          NorthEast = DefaultLights.Yellow,
          East = DefaultLights.Yellow
        };
        lYellowTransition.Rumbles = DefaultRumbleSections.Off;
        lYellowTransition.Rumbles.FadeTime = 1000;

        var lYellow = BasicFrame;
        lYellow.IsRepeated = false;
        lYellow.Length = 2000;
        lYellow.Lights = new LightSection
        {
          FadeTime = 0,
          West = DefaultLights.Yellow,
          NorthWest = DefaultLights.Yellow,
          North = DefaultLights.Yellow,
          NorthEast = DefaultLights.Yellow,
          East = DefaultLights.Yellow
        };
        lYellow.Fans = DefaultFanSections.Quarter;
        lYellow.Rumbles = DefaultRumbleSections.SoftThunder;

        var lPostYellow = BasicFrame;
        lPostYellow.IsRepeated = false;
        lPostYellow.Length = 2000;
        lPostYellow.Lights = new LightSection
        {
          FadeTime = 2000,
          West = DefaultLights.SoftYellow,
          NorthWest = DefaultLights.SoftYellow,
          North = DefaultLights.SoftYellow,
          NorthEast = DefaultLights.SoftYellow,
          East = DefaultLights.SoftYellow
        };
        lPostYellow.Fans = DefaultFanSections.Off;
        lPostYellow.Rumbles = DefaultRumbleSections.Off;

        lScene.Frames = new List<Frame> {lPreYellow, lYellowTransition, lYellow, lPostYellow};
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

using System.Collections.Generic;
using aPC.Common.Builders;
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
        lScene.Frames = new FrameBuilder()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Red)
            .Build())
          .Build();

        return lScene;
      }
    }

    public amBXScene BuildSuccess
    {
      get
      {
        var lScene = BasicScene;
        lScene.Frames = new FrameBuilder()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Green)
            .Build())
          .Build();
        return lScene;
      }
    }

    public amBXScene Building
    {
      get
      {
        var lScene = BasicScene;
        lScene.Frames = new FrameBuilder()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Yellow)
            .Build())

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Off)
            .Build())
          .Build();

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
        lPrePink.Lights = new LightSectionBuilder()
          .WithFadeTime(100)
          .WithAllLights(DefaultLights.SoftPink)
          .Build(); 
        lPrePink.Rumbles = DefaultRumbleSections.Off;

        var lPurple = BasicFrame;
        lPurple.IsRepeated = false;
        lPurple.Length = 1000;
        lPurple.Lights = new LightSectionBuilder()
          .WithFadeTime(200)
          .WithAllLights(DefaultLights.StrongPurple)
          .Build();

        lPurple.Fans = DefaultFanSections.Full;
        lPurple.Rumbles = DefaultRumbleSections.Thunder;


        var lPostPink = BasicFrame;
        lPostPink.IsRepeated = false;
        lPostPink.Length = 500;
        lPostPink.Lights = new LightSectionBuilder()
          .WithFadeTime(100)
          .WithAllLights(DefaultLights.SoftPink)
          .Build(); 
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
        lPreYellow.Lights = new LightSectionBuilder()
          .WithFadeTime(100)
          .WithAllLights(DefaultLights.SoftYellow)
          .Build();

        lPreYellow.Rumbles = DefaultRumbleSections.Off;
        lPreYellow.Fans = DefaultFanSections.Off;

        var lYellowTransition = BasicFrame;
        lYellowTransition.IsRepeated = false;
        lYellowTransition.Length = 500;
        lYellowTransition.Lights = new LightSectionBuilder()
          .WithFadeTime(500)
          .WithAllLights(DefaultLights.Yellow)
          .Build();

        lYellowTransition.Rumbles = DefaultRumbleSections.Off;
        lYellowTransition.Rumbles.FadeTime = 1000;

        var lYellow = BasicFrame;
        lYellow.IsRepeated = false;
        lYellow.Length = 2000;
        lYellow.Lights = new LightSectionBuilder()
          .WithFadeTime(0)
          .WithAllLights(DefaultLights.Yellow)
          .Build();

        lYellow.Fans = DefaultFanSections.Quarter;
        lYellow.Rumbles = DefaultRumbleSections.SoftThunder;

        var lPostYellow = BasicFrame;
        lPostYellow.IsRepeated = false;
        lPostYellow.Length = 2000;
        lPostYellow.Lights = new LightSectionBuilder()
          .WithFadeTime(2000)
          .WithAllLights(DefaultLights.SoftYellow)
          .Build();

        lPostYellow.Fans = DefaultFanSections.Off;
        lPostYellow.Rumbles = DefaultRumbleSections.Off;

        lScene.Frames = new List<Frame> {lPreYellow, lYellowTransition, lYellow, lPostYellow};
        return lScene;
      }
    }

    #endregion

    public amBXScene Support_JIRADay
    {
      get
      {
        var lScene = BasicScene;
        lScene.IsEvent = true;
        lScene.IsExclusive = true;
        lScene.IsSynchronised = true;

        var lPreBlue  = BasicFrame;
        lPreBlue.IsRepeated = false;
        lPreBlue.Length = 1000;
        lPreBlue.Lights = new LightSectionBuilder()
          .WithFadeTime(1000)
          .WithAllLights(DefaultLights.Blue)
          .Build();
        lPreBlue.Rumbles = DefaultRumbleSections.Off;
        lPreBlue.Fans = DefaultFanSections.Off;

        var lJiraBlue = BasicFrame;
        lJiraBlue.IsRepeated = false;
        lJiraBlue.Length = 5 * 1000;
        lJiraBlue.Lights = new LightSectionBuilder()
          .WithFadeTime(3000)
          .WithAllLights(DefaultLights.JiraBlue)
          .Build();
        lJiraBlue.Fans = DefaultFanSections.Quarter;

        var lPostBlue = BasicFrame;
        lPostBlue.IsRepeated = false;
        lPostBlue.Length = 10 * 1000;
        lPostBlue.Fans = DefaultFanSections.Off;

        lScene.Frames = new List<Frame> { lPreBlue, lJiraBlue, lPostBlue };
        return lScene;
      }
    }

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

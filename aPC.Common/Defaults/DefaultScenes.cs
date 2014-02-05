using System.Collections.Generic;
using System.Linq;
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
        var lScene = new amBXScene();
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Red)
          .Build();

        return lScene;
      }
    }

    public amBXScene BuildSuccess
    {
      get
      {
        var lScene = new amBXScene();
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Green)
          .Build();
        return lScene;
      }
    }

    public amBXScene Building
    {
      get
      {
        var lScene = new amBXScene();
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Yellow)

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Green)
          .Build();

        return lScene;
      }
    }

    public amBXScene BuildBrokenAndBuilding
    {
      get
      {
        var lScene = new amBXScene();
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Orange)

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Red)
          .Build();

        return lScene;
      }
    }

    public amBXScene BuildNotConnected
    {
      get
      {
        var lScene = new amBXScene();
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Off)
          .Build();

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
        var lScene = new amBXScene
                     {
                       IsSynchronised = true
                     };

        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(2000)
          .WithLightSection(new LightSectionBuilder()
            .WithFadeTime(100)
            .WithLightInDirection(eDirection.West, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.NorthWest, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.North, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.NorthEast, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.East, DefaultLights.SoftPurple)
            .Build())
          .WithFanSection(DefaultFanSections.Full)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(10)
          .WithFanSection(DefaultFanSections.Off)

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithFadeTime(100)
            .WithLightInDirection(eDirection.West, DefaultLights.Red)
            .WithLightInDirection(eDirection.NorthWest, DefaultLights.Red)
            .WithLightInDirection(eDirection.North, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.NorthEast, DefaultLights.Blue)
            .WithLightInDirection(eDirection.East, DefaultLights.Blue)
            .Build())

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithFadeTime(100)
            .WithLightInDirection(eDirection.West, DefaultLights.Blue)
            .WithLightInDirection(eDirection.NorthWest, DefaultLights.Blue)
            .WithLightInDirection(eDirection.North, DefaultLights.SoftPurple)
            .WithLightInDirection(eDirection.NorthEast, DefaultLights.Red)
            .WithLightInDirection(eDirection.East, DefaultLights.Red)
            .Build())
          .Build();

        return lScene;
      }
    }

    public amBXScene Empty
    {
      get
      {
        var lScene = new amBXScene();
        lScene.Frames = new List<Frame>();
        return lScene;
      }
    }

    public amBXScene Error_Flash
    {
      get
      {
        var lScene = new amBXScene
                     {
                       IsEvent = true,
                       IsSynchronised = true
                     };

        var lOff = new FrameBuilder()
          .AddFrame()
          .WithFrameLength(100)
          .WithLightSection(new LightSectionBuilder()
            .WithFadeTime(10)
            .WithAllLights(DefaultLights.Off)
            .Build())
          .Build()
          .Single();
        
        var lError = new FrameBuilder()
          .AddFrame()
          .WithFrameLength(200)
          .WithLightSection(new LightSectionBuilder()
            .WithFadeTime(10)
            .WithAllLights(DefaultLights.Red)
            .Build())
          .Build()
          .Single();
        
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
        var lScene = new amBXScene
                     {
                       IsEvent = true,
                       IsExclusive = true,
                       IsSynchronised = true
                     };

        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(200)
          .WithLightSection(DefaultLightSections.SoftPink)
          .WithRumbleSection(DefaultRumbleSections.Off)
          
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.StrongPurple)
          .WithFanSection(DefaultFanSections.Full)
          .WithRumbleSection(DefaultRumbleSections.Thunder)
          
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(500)
          .WithLightSection(DefaultLightSections.SoftPink)
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.Off)
          .Build();

        return lScene;
      }
    }

    #endregion

    #region Shiprec

    public amBXScene Shiprec_Praise
    {
      get
      {
        var lScene = new amBXScene
                     {
                       IsEvent = true, 
                       IsExclusive = true, 
                       IsSynchronised = true
                     };

        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(200)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.SoftYellow)
            .WithFadeTime(100)
            .Build())
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.Off)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(500)
          .WithLightSection(DefaultLightSections.Yellow)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(2000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Yellow)
            .WithFadeTime(0)
            .Build())
          .WithFanSection(DefaultFanSections.Quarter)
          .WithRumbleSection(DefaultRumbleSections.SoftThunder)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(2000)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.SoftYellow)
            .WithFadeTime(2000)
            .Build())
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.Off)
          .Build();

        return lScene;
      }
    }

    #endregion

    public amBXScene Support_JIRADay
    {
      get
      {
        var lScene = new amBXScene
                     {
                       IsEvent = true,
                       IsExclusive = true,
                       IsSynchronised = true
                     };
        lScene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Blue)
          .WithRumbleSection(DefaultRumbleSections.Off)
          .WithFanSection(DefaultFanSections.Off)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(5*1000)
          .WithLightSection(DefaultLightSections.JiraBlue)
          .WithFanSection(DefaultFanSections.Quarter)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(10*1000)
          .WithFanSection(DefaultFanSections.Off)
          .Build();

        return lScene;
      }
    }
  }
}

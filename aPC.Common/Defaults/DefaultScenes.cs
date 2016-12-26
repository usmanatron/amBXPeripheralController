using aPC.Common.Builders;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Defaults
{
  public class DefaultScenes
  {
    #region CruiseControl.NET Defaults

    [SceneName("ccnet_red")]
    public amBXScene BuildBroken
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Red)
          .Build();

        return scene;
      }
    }

    [SceneName("ccnet_green")]
    public amBXScene BuildSuccess
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Green)
          .Build();
        return scene;
      }
    }

    [SceneName("ccnet_flashingyellow")]
    public amBXScene Building
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Yellow)

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Green)
          .Build();

        return scene;
      }
    }

    [SceneName("ccnet_flashingorange")]
    public amBXScene BuildBrokenAndBuilding
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Orange)

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Red)
          .Build();

        return scene;
      }
    }

    [SceneName("ccnet_grey")]
    public amBXScene BuildNotConnected
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Off)
          .Build();

        return scene;
      }
    }

    #endregion CruiseControl.NET Defaults

    #region Misc

    [SceneName("Server_Startup")]
    public amBXScene ServerStartup
    {
      get
      {
        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Singular,
                       IsExclusive = true
                     };

        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(500)
          .WithLightSection(DefaultLightSections.JiraBlue)
          .WithFanSection(DefaultFanSections.Half)
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(750)
          .WithLightSection(DefaultLightSections.Blue)
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.SoftThunder)
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Violet)
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.Off)
          .Build();

        return scene;
      }
    }

    [SceneName("rainbow")]
    public amBXScene Rainbow
    {
      get
      {
        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Composite,
                       IsExclusive = true
                     };

        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Red)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Orange)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Yellow)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Green)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Blue)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Indigo)
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(DefaultLightSections.Violet)
          .Build();

        return scene;
      }
    }

    [SceneName("default_redvsblue")]
    public amBXScene DefaultRedVsBlue
    {
      get
      {
        var physicalDirections = new List<eDirection>
        {
          eDirection.West,
          eDirection.NorthWest,
          eDirection.North,
          eDirection.NorthEast,
          eDirection.East,
        };

        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Singular
                     };

        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(2000)
          .WithLightSection(new LightSectionBuilder()
            .WithLightInDirections(physicalDirections, DefaultLights.SoftPurple)
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
            .WithLightInDirections(new List<eDirection> { eDirection.West, eDirection.NorthWest }, DefaultLights.Red)
            .WithLightInDirection(eDirection.North, DefaultLights.SoftPurple)
            .WithLightInDirections(new List<eDirection> { eDirection.NorthEast, eDirection.East }, DefaultLights.Blue)
            .Build())

          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(1000)
          .WithLightSection(new LightSectionBuilder()
            .WithLightInDirections(new List<eDirection> { eDirection.West, eDirection.NorthWest }, DefaultLights.Blue)
            .WithLightInDirection(eDirection.North, DefaultLights.SoftPurple)
            .WithLightInDirections(new List<eDirection> { eDirection.NorthEast, eDirection.East }, DefaultLights.Red)
            .Build())
          .Build();

        return scene;
      }
    }

    [SceneName("empty")]
    public amBXScene Empty
    {
      get
      {
        var scene = new amBXScene();
        scene.Frames = new List<Frame>();
        return scene;
      }
    }

    [SceneName("error_flash")]
    public amBXScene Error_Flash
    {
      get
      {
        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Singular
                     };

        var off = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(100)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Off)
            .Build())
          .Build()
          .Single();

        var error = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(200)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Red)
            .Build())
          .Build()
          .Single();

        scene.Frames = new List<Frame> { off, error, off, error, off };
        return scene;
      }
    }

    [SceneName("fans_quarter")]
    public amBXScene QuarterFans
    {
      get
      {
        var scene = new amBXScene
          {
            SceneType = eSceneType.Composite
          };

        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(true)
          .WithFrameLength(2000)
          .WithFanSection(DefaultFanSections.Quarter)
          .Build();

        return scene;
      }
    }

    #endregion Misc

    #region PoolQ2

    [SceneName("poolq2_event")]
    public amBXScene PoolQ2_Event
    {
      get
      {
        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Singular,
                       IsExclusive = true,
                     };

        scene.Frames = new FrameBuilder()
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

        return scene;
      }
    }

    #endregion PoolQ2

    #region Shiprec

    [SceneName("shiprec_praise")]
    public amBXScene Shiprec_Praise
    {
      get
      {
        var scene = new amBXScene
                     {
                       SceneType = eSceneType.Singular,
                       IsExclusive = true,
                     };

        scene.Frames = new FrameBuilder()
          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(500)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Yellow)
            .Build())
          .WithFanSection(DefaultFanSections.Half)
          .WithRumbleSection(DefaultRumbleSections.SoftThunder)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(250)
          .WithLightSection(DefaultLightSections.JiraBlue)
          .WithFanSection(DefaultFanSections.Full)
          .WithRumbleSection(DefaultRumbleSections.Thunder)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(500)
          .WithLightSection(new LightSectionBuilder()
            .WithAllLights(DefaultLights.Yellow)
            .Build())
          .WithFanSection(DefaultFanSections.Quarter)
          .WithRumbleSection(DefaultRumbleSections.SoftThunder)

          .AddFrame()
          .WithRepeated(false)
          .WithFrameLength(250)
          .WithLightSection(DefaultLightSections.SoftYellow)
          .WithFanSection(DefaultFanSections.Off)
          .WithRumbleSection(DefaultRumbleSections.Off)
          .Build();

        return scene;
      }
    }

    #endregion Shiprec
  }
}
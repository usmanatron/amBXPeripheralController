using System.Collections.Generic;
using Common.Entities;

namespace Common
{
  class IntegratedamBXScenes
  {

    #region Private Helper Members

    #region Light

    private static readonly Light mRedLight = new Light { Intensity = 1, Red = 1, Green = 0, Blue = 0 };
    private static readonly Light mGreenLight = new Light { Intensity = 1, Red = 0, Green = 1, Blue = 0 };
    private static readonly Light mYellowLight = new Light { Intensity = 1, Red = 1, Green = 1, Blue = 0 };
    private static readonly Light mOrangeLight = new Light { Intensity = 1, Red = 1, Green = 0.5f, Blue = 0 };
    private static readonly Light mLightOff = new Light { Intensity = 0, Red = 0, Green = 0, Blue = 0 };

    private static readonly Light mBlueLight = new Light { Intensity = 1, Red = 0, Green = 0, Blue = 1 };
    private static readonly Light mSoftPurpleLight = new Light { Intensity = 0.5f, Red = 0.5f, Green = 0, Blue = 0.5f };

    #endregion

    #region LightComponent

    private LightComponent mRedLightComponent = new LightComponent
    {
      FadeTime = 500,
      North = mRedLight,
      NorthEast = mRedLight,
      East = mRedLight,
      SouthEast = mRedLight,
      South = mRedLight,
      SouthWest = mRedLight,
      West = mRedLight,
      NorthWest = mRedLight,
    };

    private LightComponent mGreenLightComponent = new LightComponent
    {
      FadeTime = 500,
      North = mGreenLight,
      NorthEast = mGreenLight,
      East = mGreenLight,
      SouthEast = mGreenLight,
      South = mGreenLight,
      SouthWest = mGreenLight,
      West = mGreenLight,
      NorthWest = mGreenLight,
    };

    private LightComponent mYellowLightComponent = new LightComponent
    {
      FadeTime = 500,
      North = mYellowLight,
      NorthEast = mYellowLight,
      East = mYellowLight,
      SouthEast = mYellowLight,
      South = mYellowLight,
      SouthWest = mYellowLight,
      West = mYellowLight,
      NorthWest = mYellowLight,
    };

    private LightComponent mOrangeLightComponent = new LightComponent
    {
      FadeTime = 500,
      North = mOrangeLight,
      NorthEast = mOrangeLight,
      East = mOrangeLight,
      SouthEast = mOrangeLight,
      South = mOrangeLight,
      SouthWest = mOrangeLight,
      West = mOrangeLight,
      NorthWest = mOrangeLight,
    };

    public static LightComponent LightsOffComponent = new LightComponent
    {
      FadeTime = 500,
      North = mLightOff,
      NorthEast = mLightOff,
      East = mLightOff,
      SouthEast = mLightOff,
      South = mLightOff,
      SouthWest = mLightOff,
      West = mLightOff,
      NorthWest = mLightOff,
    };

    #endregion

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

    #region CruiseControl.NET Defaults

    public amBXScene BuildBroken
    {
      get
      {
        var lScene = BasicScene;
        var lFrame = BasicFrame;
        lFrame.Lights = mRedLightComponent;
        lScene.Frames = new List<Frame> { BasicFrame };
        return lScene;
      }
    }

    public amBXScene BuildSuccess
    {
      get
      {
        var lScene = BasicScene;
        var lFrame = BasicFrame;
        lFrame.Lights = mGreenLightComponent;
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
        lFrameOn.Lights = mYellowLightComponent;
        lFrameOff.Lights = LightsOffComponent;

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
        lFrameOn.Lights = mOrangeLightComponent;
        lFrameOff.Lights = LightsOffComponent;

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
        lFrame.Lights = LightsOffComponent;

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
        var lPurple = BasicFrame;
        var lRedBlue = BasicFrame;
        var lBlueRed = BasicFrame;

        lPurple.IsRepeated = true;
        lPurple.Length = 2000;
        lPurple.Lights = new LightComponent
                         {
                           FadeTime = 100,
                           West = mSoftPurpleLight,
                           NorthWest = mSoftPurpleLight,
                           North = mSoftPurpleLight,
                           NorthEast = mSoftPurpleLight,
                           East = mSoftPurpleLight
                         };

        lRedBlue.Lights = new LightComponent
                          {
                            FadeTime = 100,
                            West = mRedLight,
                            NorthWest = mRedLight,
                            North = mSoftPurpleLight,
                            NorthEast = mBlueLight,
                            East = mBlueLight
                          };
        lBlueRed.Lights = new LightComponent
                          {
                            FadeTime = 100,
                            West = mBlueLight,
                            NorthWest = mBlueLight,
                            North = mSoftPurpleLight,
                            NorthEast = mRedLight,
                            East = mRedLight
                          };

        lScene.Frames = new List<Frame> {lPurple, lRedBlue, lBlueRed};
        return lScene;
      }
    }

    #endregion

  }
}

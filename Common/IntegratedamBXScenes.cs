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

    #region LightFrame

    private LightFrame mRedLightFrame = new LightFrame
    {
      IsRepeated = true,
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

    private LightFrame mGreenLightFrame = new LightFrame
    {
      IsRepeated = true,
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

    private LightFrame mYellowLightFrame = new LightFrame
    {
      IsRepeated = true,
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

    private LightFrame mOrangeLightFrame = new LightFrame
    {
      IsRepeated = true,
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

    public static LightFrame LightsOffFrame = new LightFrame
    {
      IsRepeated = true,
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

    private amBXScene BasicScene = new amBXScene
    {
      FrameLength = 1000,
      IsExclusive = true,
      Version = 1,
      LightFrames = new List<LightFrame>(),
      FanFrames = new List<FanFrame>(),
      RumbleFrames = new List<RumbleFrame>()
    };
    
    #endregion

    #region CruiseControl.NET Defaults

    public amBXScene BuildBroken
    {
      get
      {
        var lScene = BasicScene;
        lScene.LightFrames = new List<LightFrame> { mRedLightFrame };
        return lScene;
      }
    }

    public amBXScene BuildSuccess
    {
      get
      {
        var lScene = BasicScene;
        lScene.LightFrames = new List<LightFrame> { mGreenLightFrame };
        return lScene;
      }
    }

    public amBXScene Building
    {
      get
      {
        var lScene = BasicScene;
        lScene.LightFrames = new List<LightFrame> { mYellowLightFrame, LightsOffFrame };
        return lScene;
      }
    }

    public amBXScene BuildBrokenAndBuilding
    {
      get
      {
        var lScene = BasicScene;
        lScene.LightFrames = new List<LightFrame> { mOrangeLightFrame, LightsOffFrame };
        return lScene;
      }
    }

    public amBXScene BuildNotConnected
    {
      get
      {
        var lScene = BasicScene;
        lScene.LightFrames = new List<LightFrame> { LightsOffFrame };
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
        lScene.FrameLength = 2000;
        lScene.LightFrames = new List<LightFrame>
                             {
                               new LightFrame
                               {
                                 IsRepeated = false,
                                 FadeTime = 100,
                                 West = mSoftPurpleLight,
                                 NorthWest = mSoftPurpleLight,
                                 North = mSoftPurpleLight,
                                 NorthEast = mSoftPurpleLight,
                                 East = mSoftPurpleLight
                               },
                               new LightFrame
                               {
                                 IsRepeated = true,
                                 FadeTime = 100,
                                 West = mRedLight,
                                 NorthWest = mRedLight,
                                 North = mSoftPurpleLight,
                                 NorthEast = mBlueLight,
                                 East = mBlueLight
                               },
                               new LightFrame
                               {
                                 IsRepeated = true,
                                 FadeTime = 100,
                                 West = mBlueLight,
                                 NorthWest = mBlueLight,
                                 North = mSoftPurpleLight,
                                 NorthEast = mRedLight,
                                 East = mRedLight
                               }
                             };
        return lScene;
      }
    }

    #endregion

  }
}

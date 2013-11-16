using System.Collections.Generic;
using Common.Entities;

namespace Server
{
  // Deals with accessing an amBX scene, because it's fairly messy at places!
  class amBXSceneManager
  {
    public amBXSceneManager(amBXScene xiScene)
    {
      mScene = xiScene;
      SetupTickers();
    }

    private void SetupTickers()
    {
      if (mScene.LightSpecified)
      {
        mLightTicker = new AtypicalFirstRunInfiniteTicker(mScene.LightFrames.Count, mScene.RepeatableLightFrames.Count);
      }

      if (mScene.FanSpecified)
      {
        mFanTicker = new AtypicalFirstRunInfiniteTicker(mScene.FanFrames.Count, mScene.RepeatableFanFrames.Count);
      }

      if (mScene.RumbleSpecified)
      {
        mRumbleTicker = new AtypicalFirstRunInfiniteTicker(mScene.RumbleFrames.Count, mScene.RepeatableRumbleFrames.Count);
      }
    }

    public LightFrame GetNextLightFrame()
    {
      List<LightFrame> lFrames = mLightTicker.IsFirstRun
        ? mScene.LightFrames 
        : mScene.RepeatableLightFrames;

      if (lFrames.Count == 0)
      {
        // Nothing repeatable after the first run OR just nothing there (this shouldnt happen though).  Just return null.
        return null;
      }

      // Get the light
      var lFrame = lFrames[mLightTicker.Index];
      mLightTicker.Advance();

      return lFrame;
    }

    public FanFrame GetNextFanFrame()
    {
      List<FanFrame> lFrames = mFanTicker.IsFirstRun
        ? mScene.FanFrames
        : mScene.RepeatableFanFrames;

      if (lFrames.Count == 0)
      {
        // Nothing repeatable \ nothing there.  Just return null.
        return null;
      }

      // Get the light
      var lFrame = lFrames[mFanTicker.Index];
      mFanTicker.Advance();

      return lFrame;
    }

    public RumbleFrame GetNextRumbleFrame()
    {
      List<RumbleFrame> lFrames = mLightTicker.IsFirstRun
        ? mScene.RumbleFrames
        : mScene.RepeatableRumbleFrames;

      if (lFrames.Count == 0)
      {
        // Nothing repeatable \ nothing there.  Just return null.
        return null;
      }

      // Get the light
      var lFrame = lFrames[mRumbleTicker.Index];
      mRumbleTicker.Advance();

      return lFrame;
    }

    public int FrameLength
    {
      get
      {
        return mScene.FrameLength;
      }
    }

    public bool IsLightEnabled
    {
      get
      {
        return mScene.LightSpecified;
      }
    }

    public bool IsFanEnabled
    {
      get
      {
        return mScene.FanSpecified;
      }
    }

    public bool IsRumbleEnabled
    {
      get
      {
        return mScene.RumbleSpecified;
      }
    }


    private readonly amBXScene mScene;
    private AtypicalFirstRunInfiniteTicker mLightTicker;
    private AtypicalFirstRunInfiniteTicker mFanTicker;
    private AtypicalFirstRunInfiniteTicker mRumbleTicker;
  }
}

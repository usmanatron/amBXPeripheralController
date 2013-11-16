using System.Linq;
using Common.Entities;

namespace Server
{
  // Deals with accessing an amBX scene, because it's fairly messy at places!
  class amBXSceneManager
  {
    public amBXSceneManager(amBXScene xiScene)
    {
      mScene = xiScene;
      mTicker = new AtypicalFirstRunInfiniteTicker(mScene.Frames.Count, mScene.RepeatableFrames.Count);
    }

    public Frame GetNextFrame()
    {
      var lFrames = mTicker.IsFirstRun
        ? mScene.Frames
        : mScene.RepeatableFrames;

      if (!lFrames.Any())
      {
        // Nothing repeatable after the first run OR just nothing there (this shouldnt happen though).  Just return null.
        return new Frame {Lights = null, Fans = null, Rumble = null, Length = 1000, IsRepeated = false};
      }
      return mScene.Frames[mTicker.Index];
    }

    public void AdvanceScene()
    {
      mTicker.Advance();
    }

    public int FrameLength
    {
      get
      {
        return mScene.Frames[mTicker.Index].Length;
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
    private readonly AtypicalFirstRunInfiniteTicker mTicker;
  }
}

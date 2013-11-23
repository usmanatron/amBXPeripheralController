using System.Linq;
using System.ServiceModel;
using Common.Entities;

namespace Server
{
  class amBXSceneManager
  {
    public amBXSceneManager(amBXScene xiScene)
    {
      if (xiScene.IsEvent)
      {
        throw new ActionNotSupportedException("The intial Scene cannot be an event!");
      }
      SetupNewScene(xiScene);
    }

    public void UpdateScene(amBXScene xiNewScene)
    {
      mPreviousScene = mCurrentScene;
      SetupNewScene(xiNewScene);
    }

    private void SetupNewScene(amBXScene xiNewScene)
    {
      mCurrentScene = xiNewScene;
      mTicker = new AtypicalFirstRunInfiniteTicker(mCurrentScene.Frames.Count, mCurrentScene.RepeatableFrames.Count);
    }

    public Frame GetNextFrame()
    {
      var lFrames = mTicker.IsFirstRun
        ? mCurrentScene.Frames
        : mCurrentScene.RepeatableFrames;

      if (!lFrames.Any())
      {
        // this can only happen in one of two cases:
        // * This isn't an event and all frames are not repeatable.
        // * there aren't any frames at all (though this should never happen)
        // Either way, return a frame which specifies everything off (as a failsafe)

        //TODO: finish changes here
        return new Frame {Lights = null, Fans = null, Rumble = null, Length = 1000, IsRepeated = false};
      }
      return lFrames[mTicker.Index];
    }

    public void AdvanceScene()
    {
      mTicker.Advance();

      if (mCurrentScene.IsEvent && mTicker.Index == 0)
      {
        // The event has completed one full cycle.  Revert to
        // previous scene
        SetupNewScene(mPreviousScene);
      }
    }

    public bool IsLightEnabled
    {
      get
      {
        return mCurrentScene.LightSpecified;
      }
    }

    public bool IsFanEnabled
    {
      get
      {
        return mCurrentScene.FanSpecified;
      }
    }

    public bool IsRumbleEnabled
    {
      get
      {
        return mCurrentScene.RumbleSpecified;
      }
    }


    private amBXScene mCurrentScene;
    private amBXScene mPreviousScene;
    private AtypicalFirstRunInfiniteTicker mTicker;
  }
}

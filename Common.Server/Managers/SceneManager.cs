using System.Linq;
using System.ServiceModel;
using Common.Accessors;
using Common.Entities;

namespace Common.Server.Managers
{
  public class SceneManager
  {
    public SceneManager(amBXScene xiScene)
    {
      if (xiScene.IsEvent)
      {
        throw new ActionNotSupportedException("The intial Scene cannot be an event!");
      }
      SetupNewScene(xiScene);
    }

    public void UpdateScene(amBXScene xiNewScene)
    {
      if (xiNewScene.IsEvent && mCurrentScene.IsEvent)
      {
        // Skip updating the previous scene, to ensure that we don't get 
        // stuck in an infinite loop of events.
      }
      else
      {
        mPreviousScene = mCurrentScene;
      }

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
        // This can only happen in one of two cases:
        // * This isn't an event and all frames are not repeatable.
        // * There aren't any frames at all (though this should never happen)
        // Either way, return a frame which specifies everything off (as a failsafe)
        return new FrameAccessor().AllOff;
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

    private amBXScene mCurrentScene;
    private amBXScene mPreviousScene;
    private AtypicalFirstRunInfiniteTicker mTicker;
  }
}

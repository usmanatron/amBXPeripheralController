using Common.Accessors;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Server.Managers
{
  public abstract class ManagerBase<T>
  {
    public ManagerBase(amBXScene xiScene)
    {
      if (xiScene.IsEvent)
      {
        throw new InvalidOperationException("The intial Scene cannot be an event!");
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
      if (SceneIsApplicable(xiNewScene))
      {
        mCurrentScene = xiNewScene;
        mTicker = new AtypicalFirstRunInfiniteTicker(mCurrentScene.Frames.Count, mCurrentScene.RepeatableFrames.Count);
      }
    }

    protected abstract bool SceneIsApplicable(amBXScene xiNewScene);

    public abstract T GetNext();

    protected Frame GetNextFrame()
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

    protected amBXScene mCurrentScene;
    protected amBXScene mPreviousScene;
    protected AtypicalFirstRunInfiniteTicker mTicker;
  }
}

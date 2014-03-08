using System;
using System.Collections.Generic;
using System.Linq;
using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.SceneHandlers
{
  //Handles the amBXScene object(s) and any interactions.
  public abstract class SceneHandlerBase<T> where T : SnapshotBase
  {
    protected SceneHandlerBase(amBXScene xiScene, Action xiEventCallback)
    {
      mEventCallback = xiEventCallback;

      mPreviousScene = xiScene;
      CurrentScene = xiScene;
      SetupNewScene(CurrentScene);
    }

    public void UpdateScene(amBXScene xiNewScene)
    {
      lock (mSceneLock)
      {
        if (xiNewScene.IsEvent && CurrentScene.IsEvent)
        {
          // Skip updating the previous scene, to ensure that we don't get 
          // stuck in an infinite loop of events.
        }
        else
        {
          mPreviousScene = CurrentScene;
        }

        SetupNewScene(xiNewScene);
      }
    }

    protected void SetupNewScene(amBXScene xiNewScene)
    {
      IsDormant = false;
      CurrentScene = xiNewScene;
      mTicker = new AtypicalFirstRunInfiniteTicker(CurrentScene.Frames.Count, CurrentScene.RepeatableFrames.Count);
    }

    public abstract T GetNextSnapshot(eDirection xiDirection);

    protected Frame GetNextFrame()
    {
      List<Frame> lFrames;

      lock (mSceneLock)
      {
        lFrames = mTicker.IsFirstRun
          ? CurrentScene.Frames
          : CurrentScene.RepeatableFrames;
      }

      if (!lFrames.Any())
      {
        // qqUMI Add a comment here about the fact that we can get here if there are no repeatable frames
        IsDormant = true;
        return new Frame();
      }
      return lFrames[mTicker.Index];
    }

    public void AdvanceScene()
    {
      lock (mSceneLock)
      {
        mTicker.Advance();

        // If we've run the scene once through, we need to check for a few special circumstances
        if (mTicker.Index == 0)
        {
          DoSceneCompletedChecks();
        }
      }
    }

    private void DoSceneCompletedChecks()
    {
      if (CurrentScene.IsEvent)
      {
        // The event has completed one full cycle.  Revert to
        // previous scene
        SetupNewScene(mPreviousScene);
        if (mEventCallback != null)
        {
          mEventCallback();
        }
      }
    }

    protected amBXScene CurrentScene;
    public bool IsDormant { get; protected set; }
    private readonly Action mEventCallback;

    private AtypicalFirstRunInfiniteTicker mTicker;
    private amBXScene mPreviousScene;
    private readonly object mSceneLock = new object();

  }
}

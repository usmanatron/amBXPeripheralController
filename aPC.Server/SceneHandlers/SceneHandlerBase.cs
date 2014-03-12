using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.SceneHandlers
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
        if (CurrentScene.SceneType == eSceneType.Event)
        {
          if (xiNewScene.SceneType == eSceneType.Event)
          {
            // Skip updating the previous scene, to ensure that we don't get 
            // stuck in an infinite loop of events.
          }
          else
          {
            // Don't interrupt the currently playing scene - instead quietly update 
            // the previous scene so that we fall back to this when the event is done.
            mPreviousScene = xiNewScene;
            return;
          }
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
        // This can happen if there are no repeatable frames.  Mark as disabled and
        // pass through an empty frame.
        Disable();
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
        if (mTicker.Index == 0 && CurrentScene.SceneType == eSceneType.Event)
        {
          EventComplete();
        }
      }
    }

    public void Disable()
    {
      IsEnabled = false;
    }

    public void Enable()
    {
      IsEnabled = true;
    }

    private void EventComplete()
    {
      SetupNewScene(mPreviousScene);
      Disable();
      mEventCallback();
    }

    protected amBXScene CurrentScene;
    public bool IsEnabled { get; set; }
    private readonly Action mEventCallback;

    private AtypicalFirstRunInfiniteTicker mTicker;
    private amBXScene mPreviousScene;
    private readonly object mSceneLock = new object();

  }
}

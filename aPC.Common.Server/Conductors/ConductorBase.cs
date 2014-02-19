﻿using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.Conductors
{
  public abstract class ConductorBase<T> where T : SnapshotBase
  {
    protected ConductorBase(EngineActorBase<T> xiActor, Action xiEventCallback)
    {
      mActor = xiActor;
      mEventCallback = xiEventCallback;

      var lScene = new SceneAccessor().GetScene("Empty");
      mPreviousScene = lScene;
      CurrentScene = lScene;
    }

    public void Run()
    {
      if (IsDormant)
      {
        // qqUMI Ideally, we would just return here and disable the Conductor (should be more performant etc.
        // at the moment, this won't work, so for now we sleep
        Thread.Sleep(1000);
        // return;
      }
      else
      {
        var lSnapshot = GetNextSnapshot();
        if (lSnapshot == null)
        {
          throw new InvalidOperationException("An error occured when retrieving the next snapshot");
        }
        mActor.ActNextFrame(lSnapshot);
        AdvanceScene();
        WaitforInterval(lSnapshot.Length);
      }
    }

    protected void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
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
      if (FramesAreApplicable(xiNewScene.Frames))
      {
        CurrentScene = xiNewScene;
        SetupCurrentScene();
      }
      else if (FramesAreApplicable(mPreviousScene.Frames))
      {
        SetupCurrentScene();
      }
      else
      {
        IsDormant = true;
      }
    }

    private void SetupCurrentScene()
    {
      IsDormant = false;
      Ticker = new AtypicalFirstRunInfiniteTicker(CurrentScene.Frames.Count, CurrentScene.RepeatableFrames.Count);
    }

    protected abstract bool FramesAreApplicable(List<Frame> xiFrames);

    public abstract T GetNextSnapshot();

    protected Frame GetNextFrame()
    {
      List<Frame> lFrames;

      lock (mSceneLock)
      {
        lFrames = Ticker.IsFirstRun
          ? CurrentScene.Frames
          : CurrentScene.RepeatableFrames;
      }

      if (!lFrames.Any())
      {
        // This should never happen
        throw new InvalidOperationException("No more applicable frames could be found - this implies the Manager should have been made dormant but was not");
      }
      return lFrames[Ticker.Index];
    }

    private void AdvanceScene()
    {
      lock (mSceneLock)
      {
        Ticker.Advance();

        // If we've run the scene once through, we need to check for a few special circumstances
        if (Ticker.Index == 0)
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
      else if (!FramesAreApplicable(CurrentScene.RepeatableFrames))
      {
        IsDormant = true;
      }
    }

    protected amBXScene CurrentScene;

    private bool IsDormant;
    private AtypicalFirstRunInfiniteTicker Ticker;
    private EngineActorBase<T> mActor;
    private amBXScene mPreviousScene;
    private readonly object mSceneLock = new object();
    private Action mEventCallback;
  }
}
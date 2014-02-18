using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.Managers
{
  public abstract class ManagerBase
  {
    protected ManagerBase(EngineActorBase xiActor, Action xiEventCallback)
    {
      mActor = xiActor;
      mEventCallback = xiEventCallback;

      var lScene = new SceneAccessor().GetScene("Empty");
      mPreviousScene = lScene;
      CurrentScene = lScene;
    }

    public void Run()
    {
      //qqUMI This is how we should do it.  HOWEVER This causes 100% CPU atm...
      if (IsDormant)
      {
        return;
      }
      else
      {
        SnapshotBase lData;

        lock (mSceneLock) //qqUMI This is crappy - change
        {
          lData = GetNextSnapshot();
          mActor.ActNextFrame(lData);
        }
        if (lData == null)
        {
          throw new InvalidOperationException("Something bad happened - should never happen qqUMI");
        }
        AdvanceScene();
        WaitforInterval(lData.Length);
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

    public abstract SnapshotBase GetNextSnapshot();

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
        throw new InvalidOperationException("qqUMI ManagerBase returned no frames");
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
    private EngineActorBase mActor;
    private amBXScene mPreviousScene;
    private readonly object mSceneLock = new object();
    private Action mEventCallback;
  }
}

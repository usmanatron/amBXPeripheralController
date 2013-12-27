using Common.Accessors;
using Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Server.Managers
{
  public abstract class ManagerBase<T>
  {
    protected ManagerBase()
      : this(new SceneAccessor().GetScene("Default_RedVsBlue"))
    {
    }

    protected ManagerBase(amBXScene xiScene)
    {
      if (xiScene.IsEvent)
      {
        throw new InvalidOperationException("The intial Scene cannot be an event!");
      }

      CurrentScene = xiScene;
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
          PreviousScene = CurrentScene;
        }

        SetupNewScene(xiNewScene);
      }
    }

    protected void SetupNewScene(amBXScene xiNewScene)
    {
      if (SceneIsApplicable(xiNewScene))
      {
        IsDormant = false;
        CurrentScene = xiNewScene;
        Ticker = new AtypicalFirstRunInfiniteTicker(CurrentScene.Frames.Count, CurrentScene.RepeatableFrames.Count);
      }
      else
      {
        IsDormant = true;
      }
    }

    protected abstract bool SceneIsApplicable(amBXScene xiNewScene);

    public abstract Data<T> GetNext();

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
        // This can only happen in one of two cases:
        // * This isn't an event and all frames are not repeatable.
        // * There aren't any frames at all (though this should never happen)
        // Either way, return a frame which specifies everything off (as a failsafe)
        return new FrameAccessor().AllOff;
      }
      return lFrames[Ticker.Index];
    }

    public void AdvanceScene()
    {
      lock (mSceneLock)
      {
        Ticker.Advance();

        if (CurrentScene.IsEvent && Ticker.Index == 0)
        {
          // The event has completed one full cycle.  Revert to
          // previous scene
          SetupNewScene(PreviousScene);
        }
      }
    }

    public bool IsDormant;
    protected amBXScene CurrentScene;
    protected amBXScene PreviousScene;
    protected AtypicalFirstRunInfiniteTicker Ticker;

    private readonly object mSceneLock = new object();
  }
}

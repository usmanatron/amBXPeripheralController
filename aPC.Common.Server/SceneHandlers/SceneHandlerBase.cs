using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.SceneHandlers
{
  /// <summary>
  ///   Handles the amBXScene object(s) and any interactions.
  /// </summary>
  /// <typeparam name="T">A snapshot</typeparam>
  public abstract class SceneHandlerBase<T> : ISceneHandler<T> where T : SnapshotBase
  {
    protected amBXScene CurrentScene;

    public bool IsEnabled { get; set; }

    private readonly Action eventCallback;
    private AtypicalFirstRunInfiniteTicker ticker;
    private amBXScene previousScene;

    protected SceneHandlerBase(amBXScene scene, Action eventCallback)
    {
      this.eventCallback = eventCallback;

      previousScene = scene;
      CurrentScene = scene;
      SetupNewScene(CurrentScene);
    }

    public void UpdateScene(amBXScene newScene)
    {
      if (CurrentScene.SceneType == eSceneType.Event)
      {
        if (newScene.SceneType == eSceneType.Event)
        {
          // Skip updating the previous scene, to ensure that we don't get
          // stuck in an infinite loop of events.
        }
        else
        {
          // Don't interrupt the currently playing scene - instead quietly update
          // the previous scene so that we fall back to this when the event is done.
          previousScene = newScene;
          return;
        }
      }
      else
      {
        previousScene = CurrentScene;
      }

      SetupNewScene(newScene);
    }

    protected void SetupNewScene(amBXScene newScene)
    {
      CurrentScene = newScene;
      ticker = new AtypicalFirstRunInfiniteTicker(CurrentScene.Frames.Count, CurrentScene.RepeatableFrames.Count);
    }

    public abstract T GetNextSnapshot(eDirection direction);

    protected Frame GetNextFrame()
    {
      List<Frame> frames;

      frames = ticker.IsFirstRun
        ? CurrentScene.Frames
        : CurrentScene.RepeatableFrames;

      if (!frames.Any())
      {
        // This can happen if there are no repeatable frames.  Mark as disabled and
        // pass through an empty frame.
        Disable();
        return new Frame();
      }

      return frames[ticker.Index];
    }

    public void AdvanceScene()
    {
      ticker.Advance();

      // If we've run the scene once through, we need to check for a few special circumstances
      if (ticker.Index == 0 && CurrentScene.SceneType == eSceneType.Event)
      {
        EventComplete();
      }
    }

    public void Disable()
    {
      IsEnabled = false;
    }

    public void Enable()
    {
      IsEnabled = true;
      ticker.Refresh();
    }

    private void EventComplete()
    {
      SetupNewScene(previousScene);
      Disable();
      eventCallback();
    }
  }
}
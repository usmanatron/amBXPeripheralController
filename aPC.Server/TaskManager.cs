using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Engine;
using aPC.Server.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.Server
{
  // Handles the masses of tasks flying around.
  public class TaskManager
  {
    private readonly EngineActor engineActor;
    private readonly RunningComponentList runningComponentList;
    private eSceneType runningSceneType;

    public TaskManager(EngineActor engineActor, RunningComponentList runningComponentList)
    {
      this.engineActor = engineActor;
      this.runningComponentList = runningComponentList;
    }

    public void RefreshTasks(PreRunComponentList preRunComponentList)
    {
      runningSceneType = preRunComponentList.SceneType;
      var components = preRunComponentList.Get(runningSceneType);

      switch (runningSceneType)
      {
        case eSceneType.Desync:
          foreach (var directionalComponent in components)
          {
            ReScheduleTask(directionalComponent);
          }
          break;
        case eSceneType.Sync:
        case eSceneType.Event:
          runningComponentList.CancelAll();
          ScheduleTask(components.Single(), 0);
          break;
      }
    }

    private void ReScheduleTask(PreRunComponenet componentWrapper)
    {
      runningComponentList.Cancel(componentWrapper.DirectionalComponent);
      ScheduleTask(componentWrapper, 0);
    }

    private void RunFrameForDirectionalComponent(PreRunComponenet componentWrapper, CancellationTokenSource cancellationToken)
    {
      runningComponentList.Remove(cancellationToken);

      var frame = GetFrame(componentWrapper);

      if (runningSceneType == eSceneType.Desync)
      {
        var component = frame.GetComponentInDirection(componentWrapper.DirectionalComponent.ComponentType, componentWrapper.DirectionalComponent.Direction);
        engineActor.UpdateComponent(component, RunMode.Asynchronous);
      }
      else
      {
        foreach (eComponentType componentType in Enum.GetValues(typeof(eComponentType)))
          foreach (eDirection direction in EnumExtensions.GetCompassDirections())
          {
            var component = frame.GetComponentInDirection(componentType, direction);
            if (component != null)
            {
              engineActor.UpdateComponent(component, RunMode.Asynchronous);
            }
          }
      }

      DoPostUpdateActions(componentWrapper, frame.Length);
    }

    private Frame GetFrame(PreRunComponenet componentWrappers)
    {
      var frames = componentWrappers.Ticker.IsFirstRun
        ? componentWrappers.Scene.Frames
        : componentWrappers.Scene.RepeatableFrames;
      return frames[componentWrappers.Ticker.Index];
    }

    private void DoPostUpdateActions(PreRunComponenet componentWrapper, int delay)
    {
      componentWrapper.Ticker.Advance();

      // When we've run the scene once through, we need to check that there are either:
      // * repeatable frames
      // * that it's not an event
      // If neither of these hold, then we terminate running by NOT scheduling the next task.
      if (componentWrapper.Ticker.Index == 0 && (componentWrapper.Scene.SceneType == eSceneType.Event || componentWrapper.Scene.RepeatableFrames.Count == 0))
      {
        return;
      }

      ScheduleTask(componentWrapper, delay);
    }

    private void ScheduleTask(PreRunComponenet componentWrapper, int delay)
    {
      var cancellationToken = new CancellationTokenSource();
      Task.Run(async delegate
                     {
                       await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken.Token);
                       RunFrameForDirectionalComponent(componentWrapper, cancellationToken);
                     }, cancellationToken.Token);

      runningComponentList.Add(new RunningComponent(cancellationToken, componentWrapper.DirectionalComponent));
    }
  }
}
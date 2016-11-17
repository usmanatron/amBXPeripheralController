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
        case eSceneType.Composite:
          foreach (DirectionalPreRunComponent directionalComponent in components)
          {
            ReScheduleTask(directionalComponent);
          }
          break;

        case eSceneType.Singular:
          runningComponentList.CancelAll();
          var component = (PreRunFrame)components.Single();
          ScheduleTask(component, 0);
          break;
      }
    }

    private void ReScheduleTask(DirectionalPreRunComponent componentWrapper)
    {
      runningComponentList.CancelComposite(componentWrapper.DirectionalComponent);
      ScheduleTask(componentWrapper, 0);
    }

    private void ScheduleTask(IPreRunComponent componentWrapper, int delay)
    {
      //qqTODO:
      // Rewrite.  Break up depending on type of componentWrapper
      var cancellationToken = new CancellationTokenSource();
      Task.Run(async delegate
                     {
                       await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken.Token);
                       runningComponentList.Remove(cancellationToken);
                       int frameLength;

                       if (componentWrapper.Scene.SceneType == eSceneType.Composite)
                       {
                         frameLength = RunFrameForDirectionalComponent_Composite((DirectionalPreRunComponent) componentWrapper);
                       }
                       else
                       {
                         frameLength = RunFrameForDirectionalComponent_Singular((PreRunFrame) componentWrapper);
                       }

                       DoPostUpdateActions(componentWrapper, frameLength);
                     }, cancellationToken.Token);

      IRunningComponent component;
      if (componentWrapper is DirectionalPreRunComponent)
      {
        component = new DirectionalRunningComponent(cancellationToken, ((DirectionalPreRunComponent)componentWrapper).DirectionalComponent);
      }
      else
      {
        component = new RunningFrame(cancellationToken);
      }

      runningComponentList.Add(component);
    }

    private int RunFrameForDirectionalComponent_Singular(PreRunFrame componentWrapper)
    {
      var frame = GetFrame(componentWrapper as IPreRunComponent);

      foreach (eComponentType componentType in Enum.GetValues(typeof(eComponentType)))
        foreach (eDirection direction in EnumExtensions.GetCompassDirections())
        {
          var component = frame.GetComponentInDirection(componentType, direction);
          if (component != null)
          {
            engineActor.UpdateComponent(component, RunMode.Asynchronous);
          }
        }

      return frame.Length;
    }

    private int RunFrameForDirectionalComponent_Composite(DirectionalPreRunComponent componentWrapper)
    {
      var frame = GetFrame(componentWrapper as IPreRunComponent);

      var directionalComponentFromWrapper = componentWrapper.DirectionalComponent;
      var component = frame
        .GetComponentInDirection(directionalComponentFromWrapper.ComponentType, directionalComponentFromWrapper.Direction);
      engineActor.UpdateComponent(component, RunMode.Asynchronous);

      return frame.Length;
    }

    private Frame GetFrame(IPreRunComponent componentWrapper)
    {
      var frames = componentWrapper.Ticker.IsFirstRun
        ? componentWrapper.Scene.Frames
        : componentWrapper.Scene.RepeatableFrames;
      return frames[componentWrapper.Ticker.Index];
    }

    private void DoPostUpdateActions(IPreRunComponent componentWrapper, int delay)
    {
      componentWrapper.Ticker.Advance();

      // When we've run the scene once through, we need to check if there are repeatable frames
      // If there aren't any, then we terminate running by not scheduling the next task.
      if (componentWrapper.Ticker.Index == 0 && !componentWrapper.Scene.HasRepeatableFrames)
      {
        return;
      }

      ScheduleTask(componentWrapper, delay);
    }
  }
}
using aPC.Common;
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

    public TaskManager(EngineActor engineActor, RunningComponentList runningComponentList)
    {
      this.engineActor = engineActor;
      this.runningComponentList = runningComponentList;
    }

    public void RefreshTasks(ComponentWrapperList preRunComponentList)
    {
      var componentGroupings = preRunComponentList.Get()
        .GroupBy(cmp => cmp.ComponentType).ToList();

      if (componentGroupings.Count() != 1)
      {
        throw new InvalidOperationException("Composite and Singular in same time qqUMI");
      }

      foreach (var componentGrouping in componentGroupings)
        foreach (var component in componentGrouping)
        {
          if (component.ComponentType == eSceneType.Composite)
          {
            ReScheduleTask((CompositeComponentWrapper) component);
          }
          else
          {
            runningComponentList.CancelAll();
            ScheduleTask((SingularComponentWrapper) component, 0);
          }
        }
    }

    private void ReScheduleTask(CompositeComponentWrapper componentWrapper)
    {
      runningComponentList.CancelComposite(componentWrapper.DirectionalComponent);
      ScheduleTask(componentWrapper, 0);
    }

    private void ScheduleTask(ComponentWrapperBase componentWrapper, int delay)
    {
      //qqTODO:
      // Rewrite.  Break up depending on type of componentWrapper
      var cancellationToken = new CancellationTokenSource();
      Task.Run(async delegate
                     {
                       await Task.Delay(TimeSpan.FromMilliseconds(delay), cancellationToken.Token);
                       runningComponentList.Remove(cancellationToken);
                       foreach (var component in componentWrapper.GetNextComponentsToRun())
                       {
                         engineActor.UpdateComponent(component, RunMode.Asynchronous);
                       }

                       DoPostUpdateActions(componentWrapper, componentWrapper.GetNextFrameLength());
                     }, cancellationToken.Token);

      componentWrapper.Run(cancellationToken);
      runningComponentList.Add(componentWrapper);
    }
    
    private void DoPostUpdateActions(ComponentWrapperBase componentWrapper, int delay)
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
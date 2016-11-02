using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.Server.Entities
{
  /// <summary>
  /// Handles the list of DirectionalComponentActions
  /// </summary>
  public class DirectionalComponentTaskList
  {
    private readonly List<DirectionalComponentTask> actions;
    private readonly object locker;

    public DirectionalComponentTaskList()
    {
      actions = new List<DirectionalComponentTask>();
      locker = new object();
    }

    public void Add(DirectionalComponentTask directionalComponent)
    {
      lock (locker)
      {
        actions.Add(directionalComponent);
      }
    }

    /// <summary>
    /// Remove the action from the list (with the given CancellationTokenSource). No
    /// attempt is made to cancel the task.
    /// </summary>
    public void Remove(CancellationTokenSource cancellationToken)
    {
      lock (locker)
      {
        actions.Remove(actions.Single(task => task.CancellationToken == cancellationToken));
      }
    }

    /// <summary>
    /// Cancels the action for the given ComponentType and Direction, before removing from the list
    /// </summary>
    public void Cancel(DirectionalComponent directionalComponent)
    {
      var action = Get(directionalComponent);

      if (action != null)
      {
        lock (locker)
        {
          action.CancellationToken.Cancel();
          actions.Remove(action);
        }
      }
    }

    private DirectionalComponentTask Get(DirectionalComponent directionalComponent)
    {
      lock (locker)
      {
        return actions.SingleOrDefault(action => action.DirectionalComponent.Equals(directionalComponent));
      }
    }

    public void CancelAll()
    {
      lock (locker)
      {
        actions.ForEach(task => task.CancellationToken.Cancel());
        actions.Clear();
      }
    }
  }
}
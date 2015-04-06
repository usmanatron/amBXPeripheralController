using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace aPC.Server.Entities
{
  /// <summary>
  /// Handles the list of DirectionalComponentActions
  /// </summary>
  public class DirectionalComponentActionList
  {
    private List<DirectionalComponentAction> actions;
    private object locker;

    public DirectionalComponentActionList()
    {
      actions = new List<DirectionalComponentAction>();
      locker = new object();
    }

    public void Add(DirectionalComponentAction directionalComponent)
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

    private DirectionalComponentAction Get(DirectionalComponent directionalComponent)
    {
      lock (locker)
      {
        return actions.SingleOrDefault(action => action.DirectionalComponent == directionalComponent);
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
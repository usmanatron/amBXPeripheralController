using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace aPC.Server.Entities
{
  /// <summary>
  /// Handles the list of DirectionalComponentActions
  /// </summary>
  public class RunningComponentList
  {
    private readonly List<RunningComponent> actions;
    private readonly ReaderWriterLockSlim locker;

    public RunningComponentList()
    {
      actions = new List<RunningComponent>();
      locker = new ReaderWriterLockSlim();
    }

    public void Add(RunningComponent runningComponent)
    {
      locker.EnterWriteLock();
      actions.Add(runningComponent);
      locker.ExitWriteLock();
    }

    /// <summary>
    /// Remove the action from the list (with the given CancellationTokenSource). No
    /// attempt is made to cancel the task.
    /// </summary>
    public void Remove(CancellationTokenSource cancellationToken)
    {
      locker.EnterWriteLock();
      actions.Remove(actions.Single(task => task.CancellationToken == cancellationToken));
      locker.ExitWriteLock();
    }

    /// <summary>
    /// Cancels the action for the given ComponentType and Direction, before removing from the list
    /// </summary>
    public void Cancel(DirectionalComponent directionalComponent)
    {
      locker.EnterUpgradeableReadLock();
      var action = Get(directionalComponent);

      if (action != null)
      {
        locker.EnterWriteLock();
          action.CancellationToken.Cancel();
          actions.Remove(action);
        locker.ExitWriteLock();
      }

      locker.ExitUpgradeableReadLock();
    }

    private RunningComponent Get(DirectionalComponent directionalComponent)
    {
      locker.EnterReadLock();
      var action =  actions.SingleOrDefault(act => act.DirectionalComponent.Equals(directionalComponent));
      locker.ExitReadLock();
      return action;
    }

    public void CancelAll()
    {
      locker.EnterWriteLock();
        actions.ForEach(task => task.CancellationToken.Cancel());
        actions.Clear();
      locker.ExitWriteLock();
    }
  }
}
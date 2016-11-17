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
    private readonly List<IRunningComponent> runningComponents;
    private readonly ReaderWriterLockSlim locker;

    public RunningComponentList()
    {
      runningComponents = new List<IRunningComponent>();
      locker = new ReaderWriterLockSlim();
    }

    public void Add(IRunningComponent runningComponent)
    {
      locker.EnterWriteLock();
      runningComponents.Add(runningComponent);
      locker.ExitWriteLock();
    }

    /// <summary>
    /// Remove the action from the list (with the given CancellationTokenSource). No
    /// attempt is made to cancel the task.
    /// </summary>
    public void Remove(CancellationTokenSource cancellationToken)
    {
      locker.EnterWriteLock();
      runningComponents.Remove(runningComponents.Single(task => task.CancellationToken == cancellationToken));
      locker.ExitWriteLock();
    }

    /// <summary>
    /// Cancels the action for the given ComponentType and Direction, before removing from the list
    /// </summary>
    public void CancelComposite(DirectionalComponent directionalComponent)
    {
      locker.EnterUpgradeableReadLock();
      var runningComponent = Get(directionalComponent);

      if (runningComponent != null)
      {
        locker.EnterWriteLock();
        runningComponent.CancellationToken.Cancel();
        runningComponents.Remove(runningComponent);
        locker.ExitWriteLock();
      }

      locker.ExitUpgradeableReadLock();
    }

    private DirectionalRunningComponent Get(DirectionalComponent directionalComponent)
    {
      locker.EnterReadLock();
      var runningComponent = 
        runningComponents.SingleOrDefault(act => ((DirectionalRunningComponent)act).DirectionalComponent.Equals(directionalComponent));
      locker.ExitReadLock();
      return (DirectionalRunningComponent)runningComponent;
    }

    public void CancelAll()
    {
      locker.EnterWriteLock();
      runningComponents.ForEach(task => task.CancellationToken.Cancel());
      runningComponents.Clear();
      locker.ExitWriteLock();
    }
  }
}
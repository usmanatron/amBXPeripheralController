using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Threading;

namespace aPC.Common.Server.Conductors
{
  public abstract class ConductorBase<T> : IConductor where T : SnapshotBase
  {
    protected abstract void Log(string xiNotification);

    private readonly Locked<bool> isRunning;
    private readonly eDirection direction;
    private readonly IActor<T> actor;
    private readonly SceneHandlerBase<T> handler;
    private readonly object sceneLock = new object();

    protected ConductorBase(eDirection direction, IActor<T> actor, SceneHandlerBase<T> handler)
    {
      isRunning = new Locked<bool>(false);
      this.direction = direction;
      this.actor = actor;
      this.handler = handler;
    }

    public void Run()
    {
      Log("About to run");
      IsRunning.Set(true);

      while (true)
      {
        if (handler.IsEnabled)
        {
          RunOnce();
        }
        else
        {
          Log("Handler disabled - Disabling conductor.");
          IsRunning.Set(false);
          break;
        }
      }

      // May possibly change in between
      if (IsRunning.Get)
      {
        Log("Conductor externally re-enabled - restarting run");
        ThreadPool.QueueUserWorkItem(_ => Run());
      }

      Log("Run complete");
    }

    public void RunOnce()
    {
      lock (sceneLock)
      {
        var snapshot = handler.GetNextSnapshot(Direction);
        if (snapshot == null)
        {
          throw new InvalidOperationException("An error occured when retrieving the next snapshot");
        }

        actor.ActNextFrame(Direction, snapshot);
        WaitforInterval(snapshot.Length);
        handler.AdvanceScene();
      }
    }

    public void UpdateScene(amBXScene scene)
    {
      lock (sceneLock)
      {
        handler.UpdateScene(scene);
      }
    }

    private void WaitforInterval(int length)
    {
      Thread.Sleep(length);
    }

    public void Disable()
    {
      handler.Disable();
    }

    public void Enable()
    {
      handler.Enable();
    }

    public Locked<bool> IsRunning
    {
      get
      {
        return isRunning;
      }
    }

    public eDirection Direction
    {
      get
      {
        return direction;
      }
    }
  }
}
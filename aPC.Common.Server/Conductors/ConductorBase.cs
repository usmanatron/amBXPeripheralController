using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using aPC.Common.Server.SceneHandlers;
using System;
using System.Threading;

namespace aPC.Common.Server.Conductors
{
  public abstract class ConductorBase<T> : IConductor where T : SnapshotBase
  {
    protected ConductorBase(eDirection xiDirection, ActorBase<T> xiActor, SceneHandlerBase<T> xiHandler)
    {
      mIsRunning = new Locked<bool>(false);
      mDirection = xiDirection;
      mActor = xiActor;
      mHandler = xiHandler;
    }

    public void Run()
    {
      Log("About to run");
      IsRunning.Set(true);

      while (true)
      {
        if (mHandler.IsEnabled)
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
      lock (mSceneLock)
      {
        var lSnapshot = mHandler.GetNextSnapshot(Direction);
        if (lSnapshot == null)
        {
          throw new InvalidOperationException("An error occured when retrieving the next snapshot");
        }

        mActor.ActNextFrame(Direction, lSnapshot);
        WaitforInterval(lSnapshot.Length);
        mHandler.AdvanceScene();
      }
    }

    public void UpdateScene(amBXScene xiScene)
    {
      lock (mSceneLock)
      {
        mHandler.UpdateScene(xiScene);
      }
    }

    private void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    public void Disable()
    {
      mHandler.Disable();
    }

    public void Enable()
    {
      mHandler.Enable();
    }

    public Locked<bool> IsRunning
    {
      get
      {
        return mIsRunning;
      }
    }

    public eDirection Direction
    {
      get
      {
        return mDirection;
      }
    }

    public abstract eComponentType ComponentType { get; }

    protected abstract void Log(string xiNotification);

    private readonly Locked<bool> mIsRunning;
    private readonly eDirection mDirection;
    private readonly ActorBase<T> mActor;
    private readonly SceneHandlerBase<T> mHandler;
    private readonly object mSceneLock = new object();
  }
}
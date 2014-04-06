using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Actors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;
using System;
using System.Threading;

namespace aPC.Server.Conductors
{
  public abstract class ConductorBase<T> : IConductor where T : SnapshotBase
  {
    protected ConductorBase(eDirection xiDirection, ActorBase<T> xiActor, SceneHandlerBase<T> xiHandler)
    {
      mIsRunningLocker = new object();
      mDirection = xiDirection;
      mActor = xiActor;
      mHandler = xiHandler;
    }

    public void Run()
    {
      Log("About to run");
      IsRunning = true;

      while (true)
      {
        if (mHandler.IsEnabled)
        {
          RunOnce();
        }
        else
        {
          Log("Handler disabled - Disabling conductor.");
          IsRunning = false;
          break;
        }
      }

      // May possibly change in between
      if (IsRunning)
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

    public bool IsRunning
    {
      get
      {
        lock (mIsRunningLocker)
        {
          return mIsRunning;
        }
      }
      set
      {
        lock (mIsRunningLocker)
        {
          mIsRunning = value;
        }
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

    private eDirection mDirection;
    private object mIsRunningLocker;
    private bool mIsRunning;
    private readonly ActorBase<T> mActor;
    private readonly SceneHandlerBase<T> mHandler;
    private readonly object mSceneLock = new object();
  }
}
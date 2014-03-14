using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.EngineActors;
using aPC.Server.Snapshots;
using aPC.Server.SceneHandlers;
using System;
using System.Threading;

namespace aPC.Server.Conductors
{
  public abstract class ConductorBase<T> where T : SnapshotBase
  {
    protected ConductorBase(eDirection xiDirection, ActorBase<T> xiActor, SceneHandlerBase<T> xiHandler)
    {
      mIsRunningLocker = new object();
      Direction = xiDirection;
      mActor = xiActor;
      mHandler = xiHandler;
    }

    public void Run()
    {
      IsRunning = true;

      while (true)
      {
        if (mHandler.IsEnabled)
        {
          RunOnce();
        }
        else
        {
          IsRunning = false;
          break;
        }
      }

      // May possibly change in between
      if (IsRunning)
      {
        ThreadPool.QueueUserWorkItem(_ => Run());
      }
    }

    public void RunOnce()
    {
      var lSnapshot = mHandler.GetNextSnapshot(Direction);
      if (lSnapshot == null)
      {
        throw new InvalidOperationException("An error occured when retrieving the next snapshot");
      }
      
      mActor.ActNextFrame(Direction, lSnapshot);
      mHandler.AdvanceScene();
      WaitforInterval(lSnapshot.Length);
    }

    public void UpdateScene(amBXScene xiScene)
    {
      mHandler.UpdateScene(xiScene);
    }

    protected void WaitforInterval(int xiLength)
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

    public eDirection Direction;

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

    private object mIsRunningLocker;
    private bool mIsRunning;

    private readonly ActorBase<T> mActor;
    private readonly SceneHandlerBase<T> mHandler;
  }
}

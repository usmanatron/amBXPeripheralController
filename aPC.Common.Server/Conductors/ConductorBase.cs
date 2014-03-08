using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Snapshots;
using aPC.Common.Server.SceneHandlers;
using System;
using System.Threading;

namespace aPC.Common.Server.Conductors
{
  public abstract class ConductorBase<T> where T : SnapshotBase
  {
    protected ConductorBase(eDirection xiDirection, EngineActorBase<T> xiActor, SceneHandlerBase<T> xiHandler)
    {
      Direction = xiDirection;
      mActor = xiActor;
      mHandler = xiHandler;
    }

    public void Run()
    {
      while (mHandler.IsEnabled)
      {
        RunOnce();
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

    public eDirection Direction;

    private readonly EngineActorBase<T> mActor;
    private readonly SceneHandlerBase<T> mHandler;
  }
}

using Common.Server.Managers;
using Common.Entities;
using System.Threading;

namespace Common.Server.Applicators
{
  public abstract class ApplicatorBase<T>
  {
    protected ApplicatorBase(EngineManager xiEngine, ManagerBase<T> xiManager)
    {
      Engine = xiEngine;
      Manager = xiManager;
    }

    public void Run()
    {
      if (!Manager.IsDormant)
      {
        lock (Manager)
        {
          ActNextFrame();
          AdvanceScene();
        }
      }
    }

    // qqUMI Make explicit that you need to add any waiting here!
    protected abstract void ActNextFrame();

    protected void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    protected void AdvanceScene()
    {
      Manager.AdvanceScene();
    }

    public void UpdateManager(amBXScene xiScene)
    {
      Manager.UpdateScene(xiScene);
    }

    protected ManagerBase<T> Manager;
    protected EngineManager Engine;
  }
}

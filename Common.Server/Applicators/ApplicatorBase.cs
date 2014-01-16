using Common.Server.Managers;
using aPC.Common.Entities;
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
      if (Manager.IsDormant)
      {
        WaitforInterval(500); //qqUMI Constantify
      }
      else
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
      lock (Manager)
      {
        Manager.UpdateScene(xiScene);
      }
    }

    protected ManagerBase<T> Manager;
    protected EngineManager Engine;
  }
}

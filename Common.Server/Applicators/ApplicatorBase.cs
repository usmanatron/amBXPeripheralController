using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amBXLib;
using Common.Server.Managers;
using System.Threading;
using Common.Entities;
using System.ComponentModel;

namespace Common.Server.Applicators
{
  public abstract class ApplicatorBase<T>
  {
    public ApplicatorBase(EngineManager xiEngine, ManagerBase<T> xiManager)
    {
      mEngine = xiEngine;
      mManager = xiManager;
    }

    public void RunAsync()
    {
      while (true)
      {
        if (mManager.IsDormant)
        {
          Thread.Sleep(1000);
        }
        else
        {
          Run();
        }
      }
    }

    public void Run()
    {
      lock (mManager)
      {
        ActNextFrame();
        AdvanceScene();
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
      mManager.AdvanceScene();
    }

    public void UpdateManager(amBXScene xiScene)
    {
      mManager.UpdateScene(xiScene);
    }

    protected ManagerBase<T> mManager;
    protected EngineManager mEngine;
  }
}

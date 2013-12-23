using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amBXLib;
using Common.Server.Managers;
using System.Threading;
using Common.Entities;

namespace Common.Server.Applicators
{
  public abstract class ApplicatorBase<T>
  {
    public ApplicatorBase(EngineManager xiEngine, ManagerBase<T> xiManager)
    {
      mEngine = xiEngine;
      mManager = xiManager;
    }

    public void Apply()
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

    public static void UpdateManager(amBXScene xiScene)
    {
      lock (mManager)
      {
        mManager.UpdateScene(xiScene);
      }
    }

    protected static ManagerBase<T> mManager;
    protected EngineManager mEngine;
  }
}

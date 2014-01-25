﻿using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  public abstract class EngineActorBase
  {
    protected EngineActorBase(EngineManager xiEngine, ManagerBase xiManager)
    {
      Engine = xiEngine;
      Manager = xiManager;
    }

    public abstract eActorType ActorType();

    public void Run()
    {
      if (Manager.IsDormant)
      {
        WaitForDefaultInterval();
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

    /// <remarks>
    ///   Note that this method should take into account any waiting that should be 
    ///   done (before acting the next frame)
    /// </remarks>
    protected abstract void ActNextFrame();

    protected void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    protected void WaitForDefaultInterval()
    {
      WaitforInterval(500);
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

    protected ManagerBase Manager;
    protected EngineManager Engine;
  }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Entities;
using aPC.Common;

namespace aPC.Server
{
  class SceneUpdateHandler
  {
    public SceneUpdateHandler(ConductorManager xiConductorManager)
    {
      mConductorManager = xiConductorManager;
    }

    public void UpdateScene(amBXScene xiScene)
    {
      DisableAllConductors();
      UpdateActors(xiScene);
    }

    private void DisableAllConductors()
    {
      mConductorManager.DisableAll();
    }










    private void UpdateActors(amBXScene xiScene)
    {
      switch (xiScene.SceneType)
      {
        case eSceneType.Desync:
        case eSceneType.Sync:
          UpdateSynchronisedActor(xiScene);
          UpdateUnsynchronisedActors(xiScene);
          break;
        case eSceneType.Event:
          UpdateSynchronisedActor(xiScene);
          break;
      }
    }

    private void UpdateSynchronisedActor(amBXScene xiScene)
    {
      mConductorManager.UpdateSync(xiScene);
    }

    private void UpdateUnsynchronisedActors(amBXScene xiScene)
    {
      mConductorManager.UpdateDeSync(xiScene);
    }

    private ConductorManager mConductorManager;
  }
}

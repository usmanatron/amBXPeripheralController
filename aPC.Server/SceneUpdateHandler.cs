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
    public SceneUpdateHandler(
      ConductorManager xiConductorManager,
      amBXScene xiScene)
    {
      mConductorManager = xiConductorManager;
      mScene = xiScene;
    }

    public void Update()
    {
      DisableAllConductors();
      UpdateActors();
    }

    private void DisableAllConductors()
    {
      mConductorManager.DisableAll();
    }

    private void UpdateActors()
    {
      switch (mScene.SceneType)
      {
        case eSceneType.Desync:
        case eSceneType.Sync:
          UpdateSynchronisedActor();
          UpdateUnsynchronisedActors();
          break;
        case eSceneType.Event:
          UpdateSynchronisedActor();
          break;
      }
    }

    private void UpdateSynchronisedActor()
    {
      mConductorManager.UpdateSync(mScene);
    }

    private void UpdateUnsynchronisedActors()
    {
      mConductorManager.UpdateDeSync(mScene);
    }

    private ConductorManager mConductorManager;
    private amBXScene mScene;
  }
}

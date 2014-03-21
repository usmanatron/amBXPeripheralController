using System;
using System.Collections.Generic;
using aPC.Server.Communication;
using aPC.Common.Entities;
using aPC.Common.Communication;

namespace aPC.Server.Tests.Communication
{
  class TestNotificationService : INotificationService
  {
    public TestNotificationService()
    {
      PushedScenes = new List<amBXScene>();
    }

    public void RunCustomScene(string xiSceneXml)
    {
      throw new NotImplementedException();
    }

    public void RunIntegratedScene(string xiSceneName)
    {
      throw new NotImplementedException();
    }

    protected void UpdateScene(amBXScene xiScene)
    {
      PushedScenes.Add(xiScene);
    }

    public List<amBXScene> PushedScenes;
  }
}

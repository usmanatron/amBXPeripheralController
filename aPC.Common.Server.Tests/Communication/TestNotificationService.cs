using System.Collections.Generic;
using aPC.Common.Server.Communication;
using aPC.Common.Entities;

namespace aPC.Common.Server.Tests.Communication
{
  class TestNotificationService : NotificationServiceBase
  {
    public TestNotificationService()
    {
      PushedScenes = new List<amBXScene>();
    }

    protected override void UpdateScene(amBXScene xiScene)
    {
      PushedScenes.Add(xiScene);
    }

    public List<amBXScene> PushedScenes;
  }
}

using Common.Entities;
using Common.Server.Communication;

namespace ServerMT.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      var lDistributor = new SceneDistributor(xiScene);
      lDistributor.Distribute();
    }
  }
}

using aPC.Common.Entities;
using Common.Server.Communication;

namespace Server.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      Server.ServerTask.Update(xiScene);
    }
  }
}

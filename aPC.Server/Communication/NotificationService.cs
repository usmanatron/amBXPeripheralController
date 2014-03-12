using aPC.Common.Entities;
using aPC.Server.Communication;

namespace aPC.Server.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      Server.ServerTask.Update(xiScene);
    }
  }
}

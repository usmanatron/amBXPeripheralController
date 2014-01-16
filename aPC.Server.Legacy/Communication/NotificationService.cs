using aPC.Common.Entities;
using aPC.Common.Server.Communication;

namespace aPC.Server.Legacy.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      ServerTask.Applicator.UpdateManager(xiScene);
    }
  }
}

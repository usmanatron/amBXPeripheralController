using Common.Accessors;
using Common.Entities;
using Common.Server.Communication;

namespace ServerMT.Communication
{
  class NotificationService : NotificationServiceBase
  {
    protected override void UpdateScene(amBXScene xiScene)
    {
      ServerMT.ServerTask.Update(xiScene, new SceneAccessor().GetScene("Empty"));
    }
  }
}

using aPC.Client.Communication;
using aPC.Common;
using aPC.Common.Communication;

namespace aPC.Client
{
  class ClientTask
  {
    public ClientTask(Settings xiSettings)
    {
      mNotificationClient = new NotificationClient();
      mSettings = xiSettings;
    }

    public void Push()
    {
      if (mSettings.IsIntegratedScene)
      {
        mNotificationClient.PushIntegratedScene(mSettings.SceneData);
      }
      else
      {
        mNotificationClient.PushCustomScene(mSettings.SceneData);
      }
    }

    private readonly Settings mSettings;
    private INotificationClient mNotificationClient;
  }
}

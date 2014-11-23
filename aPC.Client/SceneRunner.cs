using aPC.Common.Communication;

namespace aPC.Client
{
  public class SceneRunner
  {
    private readonly Settings settings;
    private readonly INotificationClient notificationClient;

    public SceneRunner(Settings settings, INotificationClient notificationClient)
    {
      this.notificationClient = notificationClient;
      this.settings = settings;
    }

    public void RunScene()
    {
      if (settings.IsIntegratedScene)
      {
        notificationClient.PushIntegratedScene(settings.SceneData);
      }
      else
      {
        notificationClient.PushCustomScene(settings.SceneData);
      }
    }
  }
}
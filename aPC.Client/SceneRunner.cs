using aPC.Common.Communication;

namespace aPC.Client
{
  public class SceneRunner
  {
    public SceneRunner(Settings xiSettings, INotificationClient xiNotificationClient)
    {
      mNotificationClient = xiNotificationClient;
      mSettings = xiSettings;
    }

    public void RunScene()
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
    private readonly INotificationClient mNotificationClient;
  }
}
using aPC.Common.Communication;
using System;

namespace aPC.Client.Shared
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
      if (!settings.IsValid)
      {
        throw new ArgumentException("Given settings is missing a scene - nothing to run!");
      }

      if (settings.Scene != null)
      {
        notificationClient.PushScene(settings.Scene);
      }
      else
      {
        notificationClient.PushSceneName(settings.SceneName);
      }
    }
  }
}
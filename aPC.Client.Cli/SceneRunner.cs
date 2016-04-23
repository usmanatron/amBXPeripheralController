using aPC.Client.Shared;
using aPC.Common.Communication;
using System;

namespace aPC.Client
{
  public class SceneRunner
  {
    private readonly INotificationClient notificationClient;

    public SceneRunner(INotificationClient notificationClient)
    {
      this.notificationClient = notificationClient;
    }

    public void RunScene(Settings settings)
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
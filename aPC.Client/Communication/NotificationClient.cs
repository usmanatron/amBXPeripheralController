using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Client.Communication
{
  public class NotificationClient : INotificationClient
  {
    public NotificationClient()
      : this(CommunicationSettings.ServiceUrlTemplate.Replace(CommunicationSettings.HostnameHolder, "localhost"))
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string xiUrl)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiUrl);
    }

    public void PushCustomScene(string xiScene)
    {
      mClient.CreateChannel().RunCustomScene(xiScene);
    }

    public void PushIntegratedScene(string xiScene)
    {
      mClient.CreateChannel().RunIntegratedScene(xiScene);
    }

    private ChannelFactory<INotificationService> mClient;
  }
}

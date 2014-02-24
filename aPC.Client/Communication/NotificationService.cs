using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Client.Communication
{
  class NotificationClient : INotificationClient
  {
    public NotificationClient()
    {
      //This client only supports pushing to localhost
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost"));
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

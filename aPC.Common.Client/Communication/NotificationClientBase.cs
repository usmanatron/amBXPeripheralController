using System;
using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Common.Client.Communication
{
  public abstract class NotificationClientBase : INotificationClient
  {
    public NotificationClientBase() 
      : this(CommunicationSettings.ServiceUrlTemplate.Replace(CommunicationSettings.HostnameHolder, "localhost"))
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClientBase(string xiUrl)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiUrl);
    }

    public abstract void PushCustomScene(string xiScene);

    public abstract void PushIntegratedScene(string xiScene);

    protected readonly ChannelFactory<INotificationService> mClient;
  }
}

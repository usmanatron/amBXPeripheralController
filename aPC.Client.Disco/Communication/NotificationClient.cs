using System;
using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Client.Disco.Communication
{
  public class NotificationClient : INotificationClient
  {
    public NotificationClient() 
      : this(CommunicationSettings.ServiceUrlTemplate.Replace(CommunicationSettings.HostnameHolder, "localhost"))
    {
    }

    public NotificationClient(string xiUrl)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiUrl);
    }

    public void PushCustomScene(string xiScene)
    {
      try
      {
        mClient.CreateChannel().RunCustomScene(xiScene);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    public void PushIntegratedScene(string xiScene)
    {
      throw new NotImplementedException("The disco task does not use integrated scenes");
    }

    private readonly ChannelFactory<INotificationService> mClient;
  }
}

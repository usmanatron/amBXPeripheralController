using System;
using aPC.Common.Entities;
using aPC.Common.Communication;
using System.ServiceModel;
using System.IO;
using System.Xml.Serialization;

namespace aPC.Common.Client.Communication
{
  public abstract class NotificationClientBase : INotificationClient
  {
    //qqUMI TODO: Remove this constructor and always ask for the host
    protected NotificationClientBase() : this("localhost")
    {
    }

    protected NotificationClientBase(string xiHost)
      : this(new EndpointAddress(CommunicationSettings.ServiceUrlTemplate.Replace(CommunicationSettings.HostnameHolder, xiHost)))
    {
    }

    // Overriding of the Url is used by tests
    protected NotificationClientBase(EndpointAddress xiAddress)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiAddress);
    }

    public void PushCustomScene(amBXScene xiScene)
    {
      PushCustomScene(SerialiseScene(xiScene));
    }

    private string SerialiseScene(amBXScene xiScene)
    {
      using (var lWriter = new StringWriter())
      {
        var lSerializer = new XmlSerializer(typeof(amBXScene));
        lSerializer.Serialize(lWriter, xiScene);
        return lWriter.ToString();
      }
    }

    public abstract void PushCustomScene(string xiScene);

    public abstract void PushIntegratedScene(string xiScene);

    protected readonly ChannelFactory<INotificationService> mClient;
  }
}

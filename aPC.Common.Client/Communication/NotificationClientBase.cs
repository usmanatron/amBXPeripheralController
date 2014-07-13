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
    protected NotificationClientBase(string xiHost) : this(
      new EndpointAddress(CommunicationSettings.GetServiceUrl(xiHost, eApplicationType.amBXPeripheralController)))
    {
    }

    // Overriding of the Url structure is used by tests
    protected NotificationClientBase(EndpointAddress xiAddress)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiAddress);
    }

    #region Interface methods

    public virtual void PushCustomScene(string xiScene)
    {
      if (!SupportsCustomScenes)
      {
        throw new InvalidOperationException("Attempted to use custom scenes, however these are unsupported!");
      }
      mClient.CreateChannel().RunCustomScene(xiScene);
    }

    public virtual void PushIntegratedScene(string xiScene)
    {
      if (!SupportsIntegratedScenes)
      {
        throw new InvalidOperationException("Attempted to use integrated scenes, however these are unsupported!");
      }
      mClient.CreateChannel().RunIntegratedScene(xiScene);
    }

    public virtual string[] GetSupportedIntegratedScenes()
    {
      if (!SupportsIntegratedScenes)
      {
        throw new InvalidOperationException("Attempted to use integrated scenes, however these are unsupported!");
      }
      return mClient.CreateChannel().GetSupportedIntegratedScenes();
    }

    #endregion

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

    private readonly ChannelFactory<INotificationService> mClient;

    protected abstract bool SupportsCustomScenes { get; }
    protected abstract bool SupportsIntegratedScenes { get; }
  }
}

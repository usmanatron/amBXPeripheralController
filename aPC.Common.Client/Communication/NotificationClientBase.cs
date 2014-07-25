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
    protected NotificationClientBase(HostnameAccessorBase xiHostnameAccessorBase) : this(
      new EndpointAddress(CommunicationSettings.GetServiceUrl(xiHostnameAccessorBase.Get(), eApplicationType.amBXPeripheralController)))
    {
      mHostnameAccessor = xiHostnameAccessorBase;
    }

    // Overriding of the Url structure is used by tests
    protected NotificationClientBase(EndpointAddress xiAddress)
    {
      UpdateClient(xiAddress);
    }

    private void UpdateClient(EndpointAddress xiAddress)
    {
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        xiAddress);
    }

    #region Hostname Handling

    public void UpdateClientIfHostnameChanged()
    {
      if (HostnameHasChanged())
      {
        UpdateClient(new EndpointAddress(CommunicationSettings.GetServiceUrl(mHostnameAccessor.Get(), eApplicationType.amBXPeripheralController)));
      }
    }

    private bool HostnameHasChanged()
    {
      return mHostnameAccessor!= null &&
            !mClient.Endpoint.Address.Uri.Host.Contains(mHostnameAccessor.Get());
    }

    #endregion

    #region Interface methods

    public virtual void PushCustomScene(string xiScene)
    {
      UpdateClientIfHostnameChanged();
      if (!SupportsCustomScenes)
      {
        ThrowUnsupportedException("custom");
      }
      mClient.CreateChannel().RunCustomScene(xiScene);
    }

    public virtual void PushIntegratedScene(string xiScene)
    {
      UpdateClientIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }
      mClient.CreateChannel().RunIntegratedScene(xiScene);
    }

    public virtual string[] GetSupportedIntegratedScenes()
    {
      UpdateClientIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }
      return mClient.CreateChannel().GetSupportedIntegratedScenes();
    }

    private void ThrowUnsupportedException(string xiSceneType)
    {
      throw new NotSupportedException("Attempted to use " + xiSceneType + " scenes, however these are unsupported!");
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

    private readonly HostnameAccessorBase mHostnameAccessor;
    private ChannelFactory<INotificationService> mClient;

    protected abstract bool SupportsCustomScenes { get; }
    protected abstract bool SupportsIntegratedScenes { get; }
  }
}

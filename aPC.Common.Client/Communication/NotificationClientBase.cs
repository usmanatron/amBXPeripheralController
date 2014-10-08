using System;
using aPC.Common.Entities;
using aPC.Common.Communication;
using System.ServiceModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace aPC.Common.Client.Communication
{
  public abstract class NotificationClientBase : INotificationClient
  {
    protected NotificationClientBase(HostnameAccessor xiHostnameAccessor) 
    {
      mHostnameAccessor = xiHostnameAccessor;
      UpdateClients();
    }

    // Overriding of the Url structure is used by tests
    protected NotificationClientBase(string xiHostname)
    {
      mHostnameAccessor = new HostnameAccessor();
      mClients = new List<ClientConnection> { CreateConnection(xiHostname, eApplicationType.aPCTest) };
    }

    private void UpdateClients()
    {
      mClients = new List<ClientConnection>();

      foreach (var lHostname in mHostnameAccessor.GetAll())
      {
        mClients.Add(CreateConnection(lHostname, eApplicationType.amBXPeripheralController));
      }
    }

    private ClientConnection CreateConnection(string xiHostname, eApplicationType xiApplicationType)
    {
      var lAddress = new EndpointAddress(CommunicationSettings.GetServiceUrl(xiHostname, xiApplicationType));
      var lClient = new ChannelFactory<INotificationService>(new BasicHttpBinding(), lAddress);
      return new ClientConnection(xiHostname, lClient);
    }


    #region Hostname Handling

    public void UpdateClientsIfHostnameChanged()
    {
      if (mHostnameAccessor.HasChangedSinceLastCheck())
      {
        UpdateClients();
      }
    }

    #endregion

    #region Interface methods

    public virtual void PushCustomScene(string xiScene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsCustomScenes)
      {
        ThrowUnsupportedException("custom");
      }

      Parallel.ForEach(mClients, client => PushCustomScene(client.Client, xiScene));
    }

    private void PushCustomScene(ChannelFactory<INotificationService> xiClient, string xiScene)
    {
      xiClient.CreateChannel().RunCustomScene(xiScene);
    }

    public virtual void PushIntegratedScene(string xiScene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }

      Parallel.ForEach(mClients, client => PushIntegratedScene(client.Client, xiScene));
    }

    private void PushIntegratedScene(ChannelFactory<INotificationService> xiClient, string xiScene)
    {
      xiClient.CreateChannel().RunIntegratedScene(xiScene);
    }

    // TODO: Should ideally ask all Servers and only return a subset!
    public virtual string[] GetSupportedIntegratedScenes()
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }
      
      return mClients.First().Client.CreateChannel().GetSupportedIntegratedScenes();
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

    private readonly HostnameAccessor mHostnameAccessor;
    private List<ClientConnection> mClients;

    protected abstract bool SupportsCustomScenes { get; }
    protected abstract bool SupportsIntegratedScenes { get; }
  }
}

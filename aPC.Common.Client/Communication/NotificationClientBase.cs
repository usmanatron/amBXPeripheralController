using aPC.Common.Communication;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace aPC.Common.Client.Communication
{
  public abstract class NotificationClientBase : INotificationClient
  {
    private readonly HostnameAccessor hostnameAccessor;
    private List<ClientConnection> clients;

    protected abstract bool SupportsCustomScenes { get; }

    protected abstract bool SupportsIntegratedScenes { get; }

    protected NotificationClientBase(HostnameAccessor hostnameAccessor)
    {
      this.hostnameAccessor = hostnameAccessor;
      UpdateClients();
    }

    // Overriding of the Url structure is used by tests
    protected NotificationClientBase(string hostname)
    {
      hostnameAccessor = new HostnameAccessor();
      clients = new List<ClientConnection> { CreateConnection(hostname, eApplicationType.aPCTest) };
    }

    private void UpdateClients()
    {
      clients = new List<ClientConnection>();

      foreach (var hostname in hostnameAccessor.GetAll())
      {
        clients.Add(CreateConnection(hostname, eApplicationType.amBXPeripheralController));
      }
    }

    private ClientConnection CreateConnection(string hostname, eApplicationType applicationType)
    {
      var address = new EndpointAddress(CommunicationSettings.GetServiceUrl(hostname, applicationType));
      var client = new ChannelFactory<INotificationService>(new BasicHttpBinding(), address);
      return new ClientConnection(hostname, client);
    }

    #region Hostname Handling

    public void UpdateClientsIfHostnameChanged()
    {
      if (hostnameAccessor.HasChangedSinceLastCheck())
      {
        UpdateClients();
      }
    }

    #endregion Hostname Handling

    #region Interface methods

    public virtual void PushCustomScene(string scene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsCustomScenes)
      {
        ThrowUnsupportedException("custom");
      }

      Parallel.ForEach(clients, client => PushCustomScene(client.Client, scene));
    }

    private void PushCustomScene(ChannelFactory<INotificationService> client, string scene)
    {
      client.CreateChannel().RunCustomScene(scene);
    }

    public virtual void PushIntegratedScene(string scene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }

      Parallel.ForEach(clients, client => PushIntegratedScene(client.Client, scene));
    }

    private void PushIntegratedScene(ChannelFactory<INotificationService> client, string scene)
    {
      client.CreateChannel().RunIntegratedScene(scene);
    }

    // TODO: Should ideally ask all Servers and only return a subset!
    public virtual string[] GetSupportedIntegratedScenes()
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsIntegratedScenes)
      {
        ThrowUnsupportedException("integrated");
      }

      return clients.First().Client.CreateChannel().GetSupportedIntegratedScenes();
    }

    private void ThrowUnsupportedException(string sceneType)
    {
      throw new NotSupportedException("Attempted to use " + sceneType + " scenes, however these are unsupported!");
    }

    #endregion Interface methods

    public void PushCustomScene(amBXScene scene)
    {
      PushCustomScene(SerialiseScene(scene));
    }

    private string SerialiseScene(amBXScene scene)
    {
      using (var lWriter = new StringWriter())
      {
        var lSerializer = new XmlSerializer(typeof(amBXScene));
        lSerializer.Serialize(lWriter, scene);
        return lWriter.ToString();
      }
    }
  }
}
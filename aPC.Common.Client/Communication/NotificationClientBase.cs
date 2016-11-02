using aPC.Common.Communication;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace aPC.Common.Client.Communication
{
  public abstract class NotificationClientBase : INotificationClient
  {
    private readonly HostnameAccessor hostnameAccessor;
    private List<ClientConnection> clients;

    protected abstract bool SupportsScenes { get; }

    protected abstract bool SupportsSceneNames { get; }

    protected abstract bool RequiresExclusivity { get; }

    protected abstract string ApplicationId { get; }

    protected NotificationClientBase(HostnameAccessor hostnameAccessor)
    {
      this.hostnameAccessor = hostnameAccessor;
      UpdateClients();

      if (RequiresExclusivity)
      {
        Register(ApplicationId);
      }
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

    protected void UpdateClientsIfHostnameChanged()
    {
      if (hostnameAccessor.HasChangedSinceLastCheck())
      {
        UpdateClients();
      }
    }

    #endregion Hostname Handling

    #region Interface methods

    public virtual void PushScene(amBXScene scene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsScenes)
      {
        ThrowUnsupportedException("custom-built");
      }

      Parallel.ForEach(clients, client => PushScene(client.Client, scene));
    }

    private void PushScene(ChannelFactory<INotificationService> client, amBXScene scene)
    {
      client.CreateChannel().RunScene(scene);
    }

    public virtual void PushSceneName(string sceneName)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsSceneNames)
      {
        ThrowUnsupportedException("named");
      }

      Parallel.ForEach(clients, client => PushScene(client.Client, sceneName));
    }

    private void PushScene(ChannelFactory<INotificationService> client, string scene)
    {
      client.CreateChannel().RunSceneName(scene);
    }

    // TODO: Should ideally ask all Servers and only return a subset!
    public virtual string[] GetAvailableScenes()
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsSceneNames)
      {
        ThrowUnsupportedException("named");
      }

      return clients.First().Client.CreateChannel().GetAvailableScenes();
    }

    public void Register(string id)
    {
      throw new NotImplementedException();
    }

    public void PushExclusive(Frame frame)
    {
      if (!RequiresExclusivity)
      {
        throw new CommunicationException(ApplicationId, "This method should only be used for clients requiring Exclusivity");
      }

      clients.ForEach(client => client.Client.CreateChannel().RunFrameExclusive(frame));
    }

    private void ThrowUnsupportedException(string sceneType)
    {
      throw new CommunicationException(ApplicationId, "Attempted to use " + sceneType + " scenes, however these are unsupported!");
    }

    #endregion Interface methods
  }
}
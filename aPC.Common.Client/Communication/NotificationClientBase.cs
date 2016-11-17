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
    private List<HostConnection> hosts;

    protected abstract bool SupportsScenes { get; }

    protected abstract bool SupportsSceneNames { get; }

    protected abstract bool RequiresExclusivity { get; }

    protected abstract string ApplicationId { get; }

    protected NotificationClientBase(HostnameAccessor hostnameAccessor)
    {
      this.hostnameAccessor = hostnameAccessor;
      UpdateHostConnections();

      if (RequiresExclusivity)
      {
        Register(ApplicationId);
      }
    }

    // Overriding of the Url structure is used by tests
    protected NotificationClientBase(string hostname)
    {
      hostnameAccessor = new HostnameAccessor();
      hosts = new List<HostConnection> { CreateConnection(hostname, eApplicationType.aPCTest) };
    }

    private void UpdateHostConnections()
    {
      hosts = new List<HostConnection>();

      foreach (var hostname in hostnameAccessor.GetAll())
      {
        hosts.Add(CreateConnection(hostname, eApplicationType.amBXPeripheralController));
      }
    }

    private HostConnection CreateConnection(string hostname, eApplicationType applicationType)
    {
      var address = new EndpointAddress(CommunicationSettings.GetServiceUrl(hostname, applicationType));
      var client = new ChannelFactory<INotificationService>(new BasicHttpBinding(), address);
      return new HostConnection(hostname, client);
    }

    protected void UpdateClientsIfHostnameChanged()
    {
      if (hostnameAccessor.HasChangedSinceLastCheck())
      {
        UpdateHostConnections();
      }
    }

    #region Interface methods

    public virtual void PushScene(amBXScene scene)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsScenes)
      {
        ThrowUnsupportedException("custom-built");
      }

      Parallel.ForEach(hosts, host => PushScene(host.HostService, scene));
    }

    private void PushScene(ChannelFactory<INotificationService> hostService, amBXScene scene)
    {
      hostService.CreateChannel().RunScene(scene);
    }

    public virtual void PushSceneName(string sceneName)
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsSceneNames)
      {
        ThrowUnsupportedException("named");
      }

      Parallel.ForEach(hosts, host => PushScene(host.HostService, sceneName));
    }

    private void PushScene(ChannelFactory<INotificationService> hostService, string scene)
    {
      hostService.CreateChannel().RunSceneName(scene);
    }

    // TODO: Should ideally ask all Servers and only return a subset!
    public virtual string[] GetAvailableScenes()
    {
      UpdateClientsIfHostnameChanged();
      if (!SupportsSceneNames)
      {
        ThrowUnsupportedException("named");
      }

      return hosts.First().HostService.CreateChannel().GetAvailableScenes();
    }

    public void Register(string id)
    {
      var results = new List<ServerRegistrationResult>();

      foreach (var host in hosts)
      {
        results.Add(host.HostService.CreateChannel().RegisterWithServer(id));
      }

      foreach (var result in results)
      {
        //TODO: Make this more robust for multiple hosts
        if (!result.Successful)
        {
          throw result.Exception;
        }
      }
    }

    public void PushExclusive(Frame frame)
    {
      if (!RequiresExclusivity)
      {
        throw new CommunicationException(ApplicationId, "This method should only be used for clients requiring Exclusivity");
      }

      hosts.ForEach(client => client.HostService.CreateChannel().RunFrameExclusive(frame));
    }

    private void ThrowUnsupportedException(string sceneType)
    {
      throw new CommunicationException(ApplicationId, "Attempted to use " + sceneType + " scenes, however these are unsupported!");
    }

    #endregion Interface methods
  }
}
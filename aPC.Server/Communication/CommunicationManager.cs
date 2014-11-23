using aPC.Common.Communication;
using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace aPC.Server.Communication
{
  public class CommunicationManager : IDisposable
  {
    public CommunicationManager(INotificationService xiNotificationService)
    {
      SetupHost(xiNotificationService);
      mHost.Open();
    }

    private void SetupHost(INotificationService xiNotificationService)
    {
      string lBaseAddress = CommunicationSettings.GetServiceUrl(Dns.GetHostName(), eApplicationType.amBXPeripheralController);

      mHost = new ServiceHost(xiNotificationService.GetType(), new Uri(lBaseAddress));

      AddHostBehaviors();
      AddEndpoint();
    }

    private void AddHostBehaviors()
    {
      mHost.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
    }

    private void AddEndpoint()
    {
      mHost.AddServiceEndpoint(typeof(INotificationService), new BasicHttpBinding(), "");
    }

    public void Close()
    {
      mHost.Close();
    }

    public void Dispose()
    {
      Close();
    }

    private ServiceHost mHost;
  }
}
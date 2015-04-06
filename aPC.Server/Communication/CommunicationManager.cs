using aPC.Common.Communication;
using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace aPC.Server.Communication
{
  public class CommunicationManager : IDisposable
  {
    private ServiceHost host;

    public CommunicationManager(INotificationService notificationService)
    {
      SetupHost(notificationService);
      host.Open();
    }

    private void SetupHost(INotificationService notificationService)
    {
      string baseAddress = CommunicationSettings.GetServiceUrl(Dns.GetHostName(), eApplicationType.amBXPeripheralController);

      host = new ServiceHost(notificationService.GetType(), new Uri(baseAddress));

      AddHostBehaviors();
      AddEndpoint();
    }

    private void AddHostBehaviors()
    {
      host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });
    }

    private void AddEndpoint()
    {
      host.AddServiceEndpoint(typeof(INotificationService), new BasicHttpBinding(), "");
    }

    public void Close()
    {
      host.Close();
    }

    public void Dispose()
    {
      Close();
    }
  }
}
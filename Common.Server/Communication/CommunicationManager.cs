using System;
using System.ServiceModel;
using aPC.Common.Communication;

namespace Common.Server.Communication
{
  public class CommunicationManager : IDisposable
  {
    public CommunicationManager(NotificationServiceBase xiNotificationService)
    {
      SetupHost(xiNotificationService);
      mHost.Open();
    }

    private void SetupHost(NotificationServiceBase xiNotificationService)
    {
      mHost = new ServiceHost(xiNotificationService.GetType());

      AddEndpoint(CommunicationSettings.ServiceUrlTemplate.
        Replace(CommunicationSettings.HostnameHolder, System.Net.Dns.GetHostName()));
    }

    private void AddEndpoint(string xiUrl)
    {
      mHost.AddServiceEndpoint(typeof(INotificationService),
                               new BasicHttpBinding(),
                               xiUrl);
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

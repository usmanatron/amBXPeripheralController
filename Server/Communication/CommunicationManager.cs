using System;
using System.ServiceModel;
using Common.Communication;

namespace Server.Communication
{
  class CommunicationManager : IDisposable
  {
    public CommunicationManager()
    {
      SetupHost();
      mHost.Open();
    }

    private void SetupHost()
    {
      mHost = new ServiceHost(typeof (NotificationService));

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

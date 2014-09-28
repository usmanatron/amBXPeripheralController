using aPC.Common.Client;
using aPC.Common.Client.Communication;
using Ninject;
using System.ServiceModel;

namespace aPC.Chromesthesia.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    [Inject]
    public NotificationClient(HostnameAccessor xiHostnameAccessor)
      : base(xiHostnameAccessor)
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(EndpointAddress xiAddress)
      : base(xiAddress)
    {
    }

    protected override bool SupportsCustomScenes
    {
      get { return true; }
    }

    protected override bool SupportsIntegratedScenes
    {
      get { return false; }
    }
  }
}

using System.Configuration;
using System.ServiceModel;
using aPC.Common.Client.Communication;

namespace aPC.Client.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient(HostnameAccessor xiHostnameAccessor) : base(xiHostnameAccessor.Get())
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(EndpointAddress xiAddress) : base(xiAddress)
    {
    }

    protected override bool SupportsCustomScenes
    {
      get { return true; }
    }

    protected override bool SupportsIntegratedScenes
    {
      get { return true; }
    }
  }
}

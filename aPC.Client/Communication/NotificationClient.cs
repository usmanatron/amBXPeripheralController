using System.Configuration;
using System.ServiceModel;
using aPC.Common.Client.Communication;
using Ninject;

namespace aPC.Client.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    [Inject]
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

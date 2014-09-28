using System.ServiceModel;
using aPC.Common.Client;
using aPC.Common.Client.Communication;

namespace aPC.Web.Helpers
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base( new HostnameAccessor())
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string xiAddress) : base(new EndpointAddress(xiAddress))
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
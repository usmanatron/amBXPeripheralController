using System.ServiceModel;
using aPC.Common.Client;
using aPC.Common.Client.Communication;
using Ninject;

namespace aPC.Client.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    [Inject]
    public NotificationClient(HostnameAccessor xiHostnameAccessor) : base(xiHostnameAccessor)
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string xiHostname) : base(xiHostname)
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

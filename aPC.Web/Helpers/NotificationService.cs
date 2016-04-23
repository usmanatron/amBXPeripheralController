using aPC.Common.Client;
using aPC.Common.Client.Communication;

namespace aPC.Web.Helpers
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient()
      : base(new HostnameAccessor())
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string hostname)
      : base(hostname)
    {
    }

    protected override bool SupportsScenes
    {
      get { return true; }
    }

    protected override bool SupportsSceneNames
    {
      get { return true; }
    }
  }
}
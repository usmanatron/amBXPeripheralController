using aPC.Common.Client;
using aPC.Common.Client.Communication;

namespace aPC.Client.Disco.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient(HostnameAccessor hostnameAccessor)
      : base(hostnameAccessor)
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
      get { return false; }
    }
  }
}
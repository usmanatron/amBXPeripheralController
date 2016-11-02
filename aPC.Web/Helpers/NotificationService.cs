using System.Reflection;
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

    protected override bool SupportsScenes => true;

    protected override bool SupportsSceneNames => true;

    protected override bool RequiresExclusivity => false;

    protected override string ApplicationId => Assembly.GetAssembly(GetType()).FullName;
  }
}
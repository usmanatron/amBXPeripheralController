using System.Reflection;
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

    protected override bool SupportsScenes => true;

    protected override bool SupportsSceneNames => false;

    protected override bool RequiresExclusivity => false;

    protected override string ApplicationId => Assembly.GetAssembly(GetType()).FullName;
  }
}
using System;
using aPC.Common.Client.Communication;

namespace aPC.Client.Disco.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base ("localhost")
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string xiUrl) : base(xiUrl)
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

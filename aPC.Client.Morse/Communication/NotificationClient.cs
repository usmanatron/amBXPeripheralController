using aPC.Common.Client.Communication;
using System;
using System.ServiceModel;

namespace aPC.Client.Morse.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base("localhost")
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

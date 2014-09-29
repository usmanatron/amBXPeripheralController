﻿using System.ServiceModel;
using aPC.Common.Client;
using aPC.Common.Client.Communication;

namespace aPC.Client.Disco.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base (new HostnameAccessor())
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
      get { return false; }
    }
  }
}

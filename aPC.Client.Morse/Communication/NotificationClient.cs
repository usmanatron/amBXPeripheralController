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

    public override void PushCustomScene(string xiScene)
    {
      mClient.CreateChannel().RunCustomScene(xiScene);
    }

    public override void PushIntegratedScene(string xiScene)
    {
      throw new InvalidOperationException("The Morse Client does not support pushing integrated scenes.");
    }
  }
}

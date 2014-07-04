using aPC.Common.Client.Communication;

namespace aPC.Client.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base()
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
      mClient.CreateChannel().RunIntegratedScene(xiScene);
    }
  }
}

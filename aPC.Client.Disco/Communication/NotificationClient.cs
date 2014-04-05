using System;
using aPC.Common.Client.Communication;
using System.ServiceModel;

namespace aPC.Client.Disco.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient() : base ()
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(string xiUrl) : base(xiUrl)
    {
    }

    public override void PushCustomScene(string xiScene)
    {
      try
      {
        mClient.CreateChannel().RunCustomScene(xiScene);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }

    public override void PushIntegratedScene(string xiScene)
    {
      throw new NotImplementedException("The disco task does not use integrated scenes");
    }
  }
}

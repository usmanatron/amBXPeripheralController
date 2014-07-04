using System.Configuration;
using System.ServiceModel;
using aPC.Common.Client.Communication;

namespace aPC.Client.Communication
{
  public class NotificationClient : NotificationClientBase
  {
    public NotificationClient()
      : base(ConfigurationManager.AppSettings["hostname"]) //qqUMI Need to get rid of this!
    {
    }

    // Overriding of the Url is used by tests
    public NotificationClient(EndpointAddress xiAddress) : base(xiAddress)
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

using aPC.Common.Communication;
using aPC.Common.Entities;
using System.Web.Http;

namespace aPC.Web.Controllers.API
{
  public class CustomController : ApiController
  {
    private INotificationClient notificationClient;

    public CustomController(INotificationClient notificationClient)
    {
      this.notificationClient = notificationClient;
    }

    // POST custom/parse
    public amBXScene Parse([FromBody] string name)
    {
      return new amBXScene();
    }
  }
}
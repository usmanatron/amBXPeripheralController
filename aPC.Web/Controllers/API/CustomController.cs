using aPC.Common.Communication;
using aPC.Common.Entities;
using System.Web.Http;

namespace aPC.Web.Controllers.API
{
  public class CustomController : ApiController
  {
    public CustomController(INotificationClient xiNotificationClient)
    {
      mNotificationClient = xiNotificationClient;
    }

    // POST custom/parse
    public amBXScene Parse([FromBody] string name)
    {
      return new amBXScene();
    }

    private INotificationClient mNotificationClient;
  }
}
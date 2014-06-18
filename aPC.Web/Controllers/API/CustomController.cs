using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aPC.Common.Communication;
using aPC.Common.Entities;

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

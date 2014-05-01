using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aPC.Web.Areas.API.Helpers;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Web.Areas.API.Models;


namespace aPC.Web.Areas.API.Controllers
{
  public class IntegratedController : ApiController
  {
    // GET api/integrated
    public IEnumerable<amBXSceneSummary> Get()
    {
      return new SceneAccessor()
        .GetAllScenes()
        .Select(scene => new amBXSceneSummary(scene));
    }

    // GET api/integrated/{name}
    public amBXScene Get(string name)
    {
      var lScene = new SceneAccessor().GetScene(name);

      if (lScene == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      return lScene;
    }

    // POST api/integrated/{name}
    public void Post([FromUri] string name)
    {
      try
      {
        var lNotificationClient = new NotificationClient();
        lNotificationClient.PushIntegratedScene(name);
      }
      catch (Exception)
      {
        throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
      }
    }
  }
}

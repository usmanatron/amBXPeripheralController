using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace aPC.Web.Controllers.API
{
  public class IntegratedController : ApiController
  {
    private readonly INotificationClient notificationClient;

    public IntegratedController(INotificationClient notificationClient)
    {
      this.notificationClient = notificationClient;
    }

    // GET api/integrated
    public IEnumerable<amBXSceneSummary> Get()
    {
      return new SceneAccessor(new DefaultScenes())
        .GetAllScenes()
        .Select(scene => new amBXSceneSummary(scene));
    }

    // GET api/integrated/{name}
    public amBXScene Get(string name)
    {
      var lScene = new SceneAccessor(new DefaultScenes()).GetScene(name);

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
        notificationClient.PushIntegratedScene(name);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }
  }
}
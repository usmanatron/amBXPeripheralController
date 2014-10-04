using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using aPC.Common.Communication;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Web.Models;
using aPC.Common.Defaults;

namespace aPC.Web.Controllers.API
{
  public class IntegratedController : ApiController
  {
    public IntegratedController(INotificationClient xiNotificationClient)
    {
      mNotificationClient = xiNotificationClient;
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
        mNotificationClient.PushIntegratedScene(name);
      }
      catch (Exception)
      {
        throw new HttpResponseException(HttpStatusCode.InternalServerError);
      }
    }

    private readonly INotificationClient mNotificationClient;
  }
}

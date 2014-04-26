using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using aPC.API.Helpers;
using aPC.Common;
using aPC.Common.Entities;
using aPC.API.Models;


namespace aPC.API.Controllers
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
    [Route("api/integrated/{name}", Name="")]
    public amBXScene Get(string name)
    {
      var lScene = new SceneAccessor().GetScene(name);

      if (lScene == null)
      {
        throw new HttpResponseException(HttpStatusCode.NotFound);
      }

      return lScene;
    }

    // POST api/values
    public void Post([FromBody]string xiSceneName)
    {

    }
  }
}

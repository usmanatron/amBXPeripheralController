using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aPC.API.Controllers
{
  public class IntegratedController : ApiController
  {
    // GET api/integrated
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }

    // GET api/integrated/{name}
    public IEnumerable<string> Get(string xiSceneName)
    {
      return new string[] { "value1", "value2" };
    }

    // POST api/values
    public void Post([FromBody]string xiSceneName)
    {

    }
  }
}

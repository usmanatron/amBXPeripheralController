using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Client.Disco;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  class DiscoTaskTests
  {

    public void RunningFor5Seconds_With60BPM_Pushes5Scenes()
    {
      var lNotificationClient = new TestNotificationClient()

      var lTask = new DiscoTask(new TestSettings(), lNotificationClient);
      

    }


    /* qqUMI
 * Need to write a load of tests:
 * 
 * DiscoTask could be tested by injecting mock scene generator and notification service - check that if we run it for 1 second
 * then the number of scenes pushed to the notification service is within some tolerance, and that they are all the same scenes
 * returned by the generator.
 * 
 */
  }
}

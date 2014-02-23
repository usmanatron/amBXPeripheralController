using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NUnit.Framework;
using aPC.Client.Disco;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  class DiscoTaskTests
  {
    //qqUMI - This is an unstable test - first run always seems to fail?
    [Test]
    public void RunningFor2Seconds_With150BPM_Pushes5Scenes()
    {
      var lNotificationClient = new TestNotificationClient();
      var lTask = new DiscoTask(new Settings(), lNotificationClient);

      var lThread = new Thread(_ => lTask.Run());
      lThread.Start();
      Thread.Sleep(5 * 400); //The standard settings pushes a scene every 400msec
      lThread.Abort();

      Assert.AreEqual(5, lNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(0, lNotificationClient.NumberOfIntegratedScenesPushed);
    }



    /* qqUMI
 * Need to write a load of tests:
 * 
 * DiscoTask could be tested by injecting mock scene generator and notification service - check that if we run it for 1 second
 * then the number of scenes pushed to the notification service is within some tolerance, 
     * 
     * and that they are all the same scenes
 * returned by the generator.
 * 
 */
  }
}

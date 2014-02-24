using System.Threading;
using System.Threading.Tasks;
using aPC.Client.Disco.Tests.Generators;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests
{
  /* qqUMI
* Need to write a load of tests:
* 
* DiscoTask could be tested by injecting mock scene generator and notification service - check that if we run it for 1 second
* then the number of scenes pushed to the notification service is within some tolerance, 
    * 
    * and that they are all the same scenes
* returned by the generator.
*/
  [TestFixture]
  class DiscoTaskTests
  {
    //qqUMI - This is an unstable test - first run always seems to fail?
    [Test]
    public void RunningFor2Seconds_With150BPM_Pushes5Scenes()
    {
      var lSettings = new Settings();
      var lSceneGenerator = new TestRandomSceneGenerator(new TestLightSectionGenerator());
      var lNotificationClient = new TestNotificationClient();

      var lDiscoTask = new DiscoTask(lSettings, lSceneGenerator, lNotificationClient);

      var lTask = new Task(lDiscoTask.Run);
      lTask.Start();
      Thread.Sleep(5 * 400); //The standard settings pushes a scene every 400msec
      
      Assert.AreEqual(5, lNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(0, lNotificationClient.NumberOfIntegratedScenesPushed);
    }
  }
}

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
    [SetUp]
    public void Setup()
    {
      var lSettings = new Settings();
      mLightSectionGenerator = new TestLightSectionGenerator();
      var lSceneGenerator = new TestRandomSceneGenerator(mLightSectionGenerator);
      mNotificationClient = new TestNotificationClient();

      mDiscoTask = new DiscoTask(lSettings, lSceneGenerator, mNotificationClient);
    }

    //qqUMI - This is an unstable test - first run always seems to fail?
    [Test]
    public void RunningFor2Seconds_With150BPM_Pushes5Scenes()
    {
      var lTask = new Task(mDiscoTask.Run);
      lTask.Start();
      Thread.Sleep(5 * 400); //The standard settings pushes a scene every 400msec
      
      Assert.AreEqual(5, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(0, mNotificationClient.NumberOfIntegratedScenesPushed);
    }

    //[Test] //qqUMI Currently broken due to serialisation issues
    public void PushingAScene_SendsTheExpectedConfiguration()
    {
      var lTask = new Task(mDiscoTask.Run);
      lTask.Start();
      Thread.Sleep(400); //The standard settings pushes a scene every 400msec

      Assert.AreEqual(1, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(mLightSectionGenerator.Generate(), mNotificationClient.CustomScenesPushed[0]);
    }

    private TestLightSectionGenerator mLightSectionGenerator;
    private TestNotificationClient mNotificationClient;
    private DiscoTask mDiscoTask;
  }
}

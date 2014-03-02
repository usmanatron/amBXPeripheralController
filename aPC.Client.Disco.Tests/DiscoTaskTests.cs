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
      mSettings = new Settings() { BPM = 240 };
      mLightSectionGenerator = new TestLightSectionGenerator();
      var lSceneGenerator = new TestRandomSceneGenerator(mLightSectionGenerator);
      mNotificationClient = new TestNotificationClient();

      mDiscoTask = new DiscoTask(mSettings, lSceneGenerator, mNotificationClient);
    }

    [Test]
    public void RunningFor1Second_With240BPM_PushesAround4Scenes()
    {
      var lTask = new Task(mDiscoTask.Run);
      lTask.Start();
      Thread.Sleep(4 * mSettings.PushInterval);
      
      // Randomly generating the scenes each time takes a bit of time, which means that it may
      // not be exact (hence checking the value is around what we expect.
      Assert.LessOrEqual(3, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.GreaterOrEqual(5, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(0, mNotificationClient.NumberOfIntegratedScenesPushed);
    }

    [Test]
    public void PushingAScene_SendsTheExpectedConfiguration()
    {
      int lInterval = (int)(2.5 * mSettings.PushInterval);
      var lTask = new Task(mDiscoTask.Run);
      
      lTask.Start();
      Thread.Sleep(mSettings.PushInterval);

      Assert.AreEqual(1, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(mLightSectionGenerator.GeneratedScene(), mNotificationClient.CustomScenesPushed[0]);
    }

    private TestLightSectionGenerator mLightSectionGenerator;
    private TestNotificationClient mNotificationClient;
    private DiscoTask mDiscoTask;
    private Settings mSettings;
  }
}

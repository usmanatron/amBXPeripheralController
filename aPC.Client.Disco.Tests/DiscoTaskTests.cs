using System.Threading;
using System.Threading.Tasks;
using aPC.Client.Disco.Tests.Generators;
using aPC.Common.Communication;
using aPC.Common.Client.Tests.Communication;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests
{
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
      int lInterval = (int)(1.5 * mSettings.PushInterval);
      var lTask = new Task(mDiscoTask.Run);
      
      lTask.Start();
      Thread.Sleep(lInterval);

      Assert.AreEqual(2, mNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(mLightSectionGenerator.GeneratedScene(), mNotificationClient.CustomScenesPushed[0]);
    }

    private TestLightSectionGenerator mLightSectionGenerator;
    private TestNotificationClient mNotificationClient;
    private DiscoTask mDiscoTask;
    private Settings mSettings;
  }
}

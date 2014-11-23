using aPC.Client.Disco.Tests.Generators;
using aPC.Common.Client;
using aPC.Common.Client.Tests.Communication;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  internal class DiscoTaskTests
  {
    private TestLightSectionGenerator lightSectionGenerator;
    private TestNotificationClient notificationClient;
    private DiscoTask discoTask;
    private Settings settings;

    [SetUp]
    public void Setup()
    {
      settings = new Settings(new HostnameAccessor()) { BPM = 240 };
      lightSectionGenerator = new TestLightSectionGenerator();
      var sceneGenerator = new TestRandomSceneGenerator(lightSectionGenerator);
      notificationClient = new TestNotificationClient();

      discoTask = new DiscoTask(settings, sceneGenerator, notificationClient);
    }

    [Test]
    public void RunningFor1Second_With240BPM_PushesAround4Scenes()
    {
      var task = new Task(discoTask.Run);
      task.Start();
      Thread.Sleep(4 * settings.PushInterval);

      // Randomly generating the scenes each time takes a bit of time, which means that it may
      // not be exact (hence checking the value is around what we expect.
      Assert.LessOrEqual(3, notificationClient.NumberOfCustomScenesPushed);
      Assert.GreaterOrEqual(5, notificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(0, notificationClient.NumberOfIntegratedScenesPushed);
    }

    [Test]
    public void PushingAScene_SendsTheExpectedConfiguration()
    {
      int interval = (int)(1.5 * settings.PushInterval);
      var task = new Task(discoTask.Run);

      task.Start();
      Thread.Sleep(interval);

      Assert.AreEqual(2, notificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(lightSectionGenerator.GeneratedScene(), notificationClient.CustomScenesPushed[0]);
    }
  }
}
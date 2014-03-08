using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace aPC.Common.Server.Tests.Communication
{
  [TestFixture]
  class NotificationServiceBaseTests
  {
    [Test]
    public void PushedCustomScene_Updated()
    {
      var lService = new TestNotificationService();
      lService.RunCustomScene(File.ReadAllText("ExampleScene.xml"));

      Assert.AreEqual(1, lService.PushedScenes.Count);
      //qq
    }

    [Test]
    public void PushedValidIntegratedScene_Updated()
    {
      var lService = new TestNotificationService();
      lService.RunIntegratedScene("Default_RedVsBlue");

      Assert.AreEqual(1, lService.PushedScenes.Count);
      //qq
    }

    [Test]
    public void PushedInvalidIntegratedScene_DefaultstoErrorFlash()
    {
      var lService = new TestNotificationService();
      lService.RunIntegratedScene("TotallyInvalidSceneName");

      Assert.AreEqual(1, lService.PushedScenes.Count);
    }
  }
}

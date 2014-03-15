using aPC.Common;
using aPC.Server;
using NUnit.Framework;

namespace aPC.Server.Tests
{
  class SceneStatusTests
  {
    [Test]
    public void NewSceneStatus_BothTypesequal()
    {
      var lStatus = new SceneStatus(eSceneType.Desync);

      Assert.AreEqual(eSceneType.Desync, lStatus.CurrentSceneType);
      Assert.AreEqual(eSceneType.Desync, lStatus.PreviousSceneType);
    }

    [Test]
    public void PreviousScene_ReflectsPreviousSetting()
    {
      var lStatus = new SceneStatus(eSceneType.Desync);
      lStatus.CurrentSceneType = eSceneType.Sync;

      Assert.AreEqual(eSceneType.Sync, lStatus.CurrentSceneType);
      Assert.AreEqual(eSceneType.Desync, lStatus.PreviousSceneType);
    }

    [Test]
    public void SceneStatus_OnlyRecallsImmediatelyPreviousStatus()
    {
      var lStatus = new SceneStatus(eSceneType.Desync);
      lStatus.CurrentSceneType = eSceneType.Sync;
      lStatus.CurrentSceneType = eSceneType.Event;

      Assert.AreEqual(eSceneType.Event, lStatus.CurrentSceneType);
      Assert.AreEqual(eSceneType.Sync, lStatus.PreviousSceneType);
    }
  }
}

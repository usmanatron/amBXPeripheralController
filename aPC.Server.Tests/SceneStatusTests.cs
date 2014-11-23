using aPC.Common;
using NUnit.Framework;

namespace aPC.Server.Tests
{
  internal class SceneStatusTests
  {
    [Test]
    public void NewSceneStatus_BothTypesequal()
    {
      var status = new SceneStatus(eSceneType.Desync);

      Assert.AreEqual(eSceneType.Desync, status.CurrentSceneType);
      Assert.AreEqual(eSceneType.Desync, status.PreviousSceneType);
    }

    [Test]
    public void PreviousScene_ReflectsPreviousSetting()
    {
      var status = new SceneStatus(eSceneType.Desync);
      status.CurrentSceneType = eSceneType.Sync;

      Assert.AreEqual(eSceneType.Sync, status.CurrentSceneType);
      Assert.AreEqual(eSceneType.Desync, status.PreviousSceneType);
    }

    [Test]
    public void SceneStatus_OnlyRecallsImmediatelyPreviousStatus()
    {
      var status = new SceneStatus(eSceneType.Desync);
      status.CurrentSceneType = eSceneType.Sync;
      status.CurrentSceneType = eSceneType.Event;

      Assert.AreEqual(eSceneType.Event, status.CurrentSceneType);
      Assert.AreEqual(eSceneType.Sync, status.PreviousSceneType);
    }
  }
}
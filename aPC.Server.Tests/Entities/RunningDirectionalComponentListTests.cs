using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Server.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.Tests.Entities
{
  [TestFixture]
  internal class RunningDirectionalComponentListTests
  {
    private RunningDirectionalComponentList runningDirectionalComponentList;
    private amBXScene arbitrarySyncScene;
    private amBXScene arbitraryDesyncScene;
    private DirectionalComponent arbitraryDirectionalComponent;

    [SetUp]
    public void Setup()
    {
      runningDirectionalComponentList = new RunningDirectionalComponentList();
      var defaultScenes = new DefaultScenes();
      arbitrarySyncScene = defaultScenes.DefaultRedVsBlue;
      arbitraryDesyncScene = defaultScenes.Rainbow;
      arbitraryDirectionalComponent = new DirectionalComponent(eComponentType.Light);
    }

    [Test]
    public void Updating_WithoutRunningStartUpdate_Throws()
    {
      Assert.Throws<InvalidOperationException>(() => runningDirectionalComponentList.Update(arbitrarySyncScene, arbitraryDirectionalComponent));
    }

    [Test]
    public void SettingSync_RetrievableViaGetSync()
    {
      runningDirectionalComponentList.StartUpdate(arbitrarySyncScene.SceneType);
      runningDirectionalComponentList.UpdateSync(arbitrarySyncScene);
      runningDirectionalComponentList.EndUpdate();

      var component = runningDirectionalComponentList.GetSync();

      Assert.AreEqual(arbitrarySyncScene, component.Scene);
      Assert.AreEqual(arbitraryDirectionalComponent, component.DirectionalComponent);
    }

    [Test]
    public void SettingScene_BuildsNewTicket()
    {
      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);
      runningDirectionalComponentList.EndUpdate();

      var component = runningDirectionalComponentList.Get(arbitraryDirectionalComponent);

      Assert.AreEqual(0, component.Ticker.Index);
      Assert.AreEqual(true, component.Ticker.IsFirstRun);
    }

    [Test]
    public void SettingDesync_RetrievableViaGet()
    {
      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);
      runningDirectionalComponentList.EndUpdate();

      var component = runningDirectionalComponentList.Get(arbitraryDirectionalComponent);

      Assert.AreEqual(arbitraryDesyncScene, component.Scene);
    }

    [Test]
    public void Clear_RemovesAllDesyncComponents()
    {
      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);

      runningDirectionalComponentList.Clear();
      runningDirectionalComponentList.EndUpdate();

      Assert.IsNull(runningDirectionalComponentList.Get(arbitraryDirectionalComponent));
    }

    [Test]
    public void CompletedUpdateCycle_SetsLastUpdatedComponentList()
    {
      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);
      runningDirectionalComponentList.EndUpdate();

      Assert.AreEqual(1, runningDirectionalComponentList.LastUpdatedDirectionalComponents.Count);
      Assert.AreEqual(arbitraryDirectionalComponent, runningDirectionalComponentList.LastUpdatedDirectionalComponents.Single());
    }

    [Test]
    public void SubsequentUpdateCycle_ResetsLastUpdatedComponentList()
    {
      var firstComponent = new DirectionalComponent(eComponentType.Light, eDirection.North);
      var secondComponent = new DirectionalComponent(eComponentType.Light, eDirection.East);

      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitrarySyncScene, firstComponent);
      runningDirectionalComponentList.EndUpdate();

      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, secondComponent);
      runningDirectionalComponentList.EndUpdate();

      Assert.AreEqual(1, runningDirectionalComponentList.LastUpdatedDirectionalComponents.Count);
      Assert.AreEqual(secondComponent, runningDirectionalComponentList.LastUpdatedDirectionalComponents.Single());
    }

    [Test]
    public void SecondUpdateCycle_DoesNotUpdateUntouchedDirectionalComponents()
    {
      var firstComponent = new DirectionalComponent(eComponentType.Light, eDirection.North);
      var secondComponent = new DirectionalComponent(eComponentType.Light, eDirection.East);

      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitrarySyncScene, firstComponent);
      runningDirectionalComponentList.EndUpdate();

      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, secondComponent);
      runningDirectionalComponentList.EndUpdate();

      Assert.AreEqual(arbitrarySyncScene, runningDirectionalComponentList.Get(firstComponent).Scene);
      Assert.AreEqual(firstComponent, runningDirectionalComponentList.Get(firstComponent).DirectionalComponent);
      Assert.AreEqual(arbitraryDesyncScene, runningDirectionalComponentList.Get(secondComponent).Scene);
      Assert.AreEqual(secondComponent, runningDirectionalComponentList.Get(secondComponent).DirectionalComponent);
    }

    [Test]
    public void UpdatingTheSameDirectionalComponent_RemovesThePreviouslySpecifiedOne()
    {
      runningDirectionalComponentList.StartUpdate(arbitraryDesyncScene.SceneType);
      runningDirectionalComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);
      runningDirectionalComponentList.Update(arbitrarySyncScene, arbitraryDirectionalComponent);
      runningDirectionalComponentList.EndUpdate();

      Assert.AreEqual(arbitrarySyncScene, runningDirectionalComponentList.Get(arbitraryDirectionalComponent).Scene);
      Assert.AreEqual(arbitraryDirectionalComponent, runningDirectionalComponentList.Get(arbitraryDirectionalComponent).DirectionalComponent);
    }
  }
}
using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Server.Entities;
using NUnit.Framework;
using System.Linq;

namespace aPC.Server.Tests.Entities
{
  [TestFixture]
  internal class RunningDirectionalComponentListTests
  {
    private PreRunComponentList preRunComponentList;
    private amBXScene arbitrarySyncScene;
    private amBXScene arbitraryDesyncScene;
    private DirectionalComponent arbitraryDirectionalComponent;

    [SetUp]
    public void Setup()
    {
      preRunComponentList = new PreRunComponentList();
      var defaultScenes = new DefaultScenes();
      arbitrarySyncScene = defaultScenes.DefaultRedVsBlue;
      arbitraryDesyncScene = defaultScenes.Rainbow;
      arbitraryDirectionalComponent = new DirectionalComponent(eComponentType.Light, eDirection.North);
    }
    
    [Test]
    public void SettingSync_RetrievableViaGetSync()
    {
      var directionalComponent = new DirectionalComponent(eComponentType.Light, eDirection.Everywhere);
      preRunComponentList.Update(arbitrarySyncScene, directionalComponent);

      var component = preRunComponentList.Get(eSceneType.Singular);

      Assert.AreEqual(arbitrarySyncScene, component.Single().Scene);
      Assert.AreEqual(directionalComponent, component.Single().DirectionalComponent);
    }

    [Test]
    public void SettingScene_BuildsNewTicket()
    {
      preRunComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);

      var component = preRunComponentList.Get(eSceneType.Composite)
        .Single(cmp => cmp.DirectionalComponent.Equals(arbitraryDirectionalComponent));

      Assert.AreEqual(0, component.Ticker.Index);
      Assert.AreEqual(true, component.Ticker.IsFirstRun);
      Assert.AreEqual(arbitraryDesyncScene, component.Scene);

    }
    
    [Test]
    public void CompletedUpdateCycle_SetsLastUpdatedComponentList()
    {
      preRunComponentList.Update(arbitraryDesyncScene, arbitraryDirectionalComponent);

      Assert.AreEqual(1, preRunComponentList.LastUpdatedDirectionalComponents.Count);
      Assert.AreEqual(arbitraryDirectionalComponent, preRunComponentList.LastUpdatedDirectionalComponents.Single());
    }

    [Test]
    public void SubsequentUpdateCycle_ResetsLastUpdatedComponentList()
    {
      var firstComponent = new DirectionalComponent(eComponentType.Light, eDirection.North);
      var secondComponent = new DirectionalComponent(eComponentType.Light, eDirection.East);

      preRunComponentList.Update(arbitrarySyncScene, firstComponent);

      preRunComponentList.Update(arbitraryDesyncScene, secondComponent);

      Assert.AreEqual(1, preRunComponentList.LastUpdatedDirectionalComponents.Count);
      Assert.AreEqual(secondComponent, preRunComponentList.LastUpdatedDirectionalComponents.Single());
    }
  }
}
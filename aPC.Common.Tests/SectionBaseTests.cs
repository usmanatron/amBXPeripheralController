using System;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests
{
  [TestFixture]
  class SectionBaseTests
  {
    [Test]
    public void GettingComponentByDirection_GivesCorrectMember()
    {
      var lSection = new TestSection();
      var lComponent = lSection.GetComponentValueInDirection(eDirection.North);
      Assert.AreEqual(lSection.Up, lComponent);
    }

    [Test]
    public void GettingComponentByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var lSection = new TestSection();
      var lNorthEast = lSection.GetComponentValueInDirection(eDirection.NorthEast);
      var lEast = lSection.GetComponentValueInDirection(eDirection.East);
      var lSouthEast = lSection.GetComponentValueInDirection(eDirection.SouthEast);

      Assert.AreEqual(lSection.Right, lNorthEast);
      Assert.AreEqual(lSection.Right, lEast);
      Assert.AreEqual(lSection.Right, lSouthEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      var lSection = new TestSection();
      Assert.Throws<InvalidOperationException>(() => lSection.GetComponentValueInDirection(eDirection.South));
    }

    [Test]
    public void SettingComponentByDirection_IsCorrectlySaved()
    {
      var lSection = new TestSection();
      var lComponent = new TestComponent { Value = "Test1" };
      var lSuccess = lSection.SetComponentValueInDirection(lComponent, eDirection.North);

      Assert.AreEqual(true, lSuccess);
      Assert.AreEqual(lComponent.Value, lSection.Up.Value);
    }

    [Test]
    public void SettingComponentByDirection_WhereDirectionDoesntExist_Fails()
    {
      var lSection = new TestSection();
      var lComponent = new TestComponent { Value = "Test2" };
      var lSuccess = lSection.SetComponentValueInDirection(lComponent, eDirection.West);

      Assert.AreEqual(false, lSuccess);
      Assert.IsNull(lSection.Down);
    }

    [Test]
    public void GettingPhysicalComponentByDirection_GivesExpectedMember()
    {
      var lSection = new TestSection();
      var lComponent = lSection.GetPhysicalComponentValueInDirection(eDirection.North);
      Assert.AreEqual(lSection.Down, lComponent);
    }

    [Test]
    public void GettingPhysicalComponentByDirection_WhereTheMemberIsNotMarkedPhysical_GivesNull()
    {
      var lSection = new TestSection();
      var lComponent = lSection.GetPhysicalComponentValueInDirection(eDirection.East);
      Assert.IsNull(lComponent);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_IsCorrectlySaved()
    {
      var lSection = new TestSection();
      var lComponent = new TestComponent { Value = "Test3" };
      var lSuccess = lSection.SetPhysicalComponentValueInDirection(lComponent, eDirection.North);

      Assert.AreEqual(true, lSuccess);
      Assert.AreEqual(lComponent.Value, lSection.Up.Value);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_WhereDirectionIsNotMarkedPhysical_Fails()
    {
      var lSection = new TestSection();
      var lComponent = new TestComponent { Value = "Test4" };
      var lSuccess = lSection.SetPhysicalComponentValueInDirection(lComponent, eDirection.East);

      Assert.AreEqual(false, lSuccess);
      Assert.IsNull(lSection.Right);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_WhereDirectionDoesntExistAtAll_Fails()
    {
      var lSection = new TestSection();
      var lComponent = new TestComponent { Value = "Test5" };
      var lSuccess = lSection.SetPhysicalComponentValueInDirection(lComponent, eDirection.West);

      Assert.AreEqual(false, lSuccess);
    }
  }

  class TestSection : SectionBase<TestComponent>
  {
#pragma warning disable 169 // Fields are used exclusively by reflection
    [PhysicalComponent]
    [Direction(eDirection.North)]
    public TestComponent Up;

    [Direction(eDirection.NorthEast)]
    [Direction(eDirection.East)]
    [Direction(eDirection.SouthEast)]
    public TestComponent Right;

    [Direction(eDirection.South)]
    public TestComponent Down;

    [Direction(eDirection.South)]
    public TestComponent DownClone;
#pragma warning restore 169
  }

  class TestComponent : IComponent
  {
    public string Value;
  }
}
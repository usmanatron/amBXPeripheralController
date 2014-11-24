using aPC.Common.Entities;
using NUnit.Framework;
using System;

namespace aPC.Common.Tests
{
  [TestFixture]
  internal class IComponentSectionExtensionTests
  {
    private TestSection testSection;

    [SetUp]
    public void Setup()
    {
      this.testSection = new TestSection();
    }

    [Test]
    public void GettingComponentByDirection_GivesCorrectMember()
    {
      var component = testSection.GetComponentValueInDirection(eDirection.North);

      Assert.AreEqual(testSection.Up, component);
    }

    [Test]
    public void GettingComponentByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var northEast = testSection.GetComponentValueInDirection(eDirection.NorthEast);
      var east = testSection.GetComponentValueInDirection(eDirection.East);
      var southEast = testSection.GetComponentValueInDirection(eDirection.SouthEast);

      Assert.AreEqual(testSection.Right, northEast);
      Assert.AreEqual(testSection.Right, east);
      Assert.AreEqual(testSection.Right, southEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      Assert.Throws<InvalidOperationException>(() => testSection.GetComponentValueInDirection(eDirection.South));
    }

    [Test]
    public void SettingComponentByDirection_IsCorrectlySaved()
    {
      var component = new TestComponent { Value = "Test1" };
      var success = testSection.SetComponentValueInDirection(component, eDirection.North);

      Assert.AreEqual(true, success);
      Assert.AreEqual(component.Value, testSection.Up.Value);
    }

    [Test]
    public void SettingComponentByDirection_WhereDirectionDoesntExist_Fails()
    {
      var component = new TestComponent { Value = "Test2" };
      var success = testSection.SetComponentValueInDirection(component, eDirection.West);

      Assert.AreEqual(false, success);
      Assert.IsNull(testSection.Down);
    }

    [Test]
    public void GettingPhysicalComponentByDirection_GivesExpectedMember()
    {
      var component = testSection.GetPhysicalComponentValueInDirection(eDirection.North);

      Assert.AreEqual(testSection.Down, component);
    }

    [Test]
    public void GettingPhysicalComponentByDirection_WhereTheMemberIsNotMarkedPhysical_GivesNull()
    {
      var component = testSection.GetPhysicalComponentValueInDirection(eDirection.East);

      Assert.IsNull(component);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_IsCorrectlySaved()
    {
      var component = new TestComponent { Value = "Test3" };
      var success = testSection.SetPhysicalComponentValueInDirection(component, eDirection.North);

      Assert.AreEqual(true, success);
      Assert.AreEqual(component.Value, testSection.Up.Value);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_WhereDirectionIsNotMarkedPhysical_Fails()
    {
      var lComponent = new TestComponent { Value = "Test4" };
      var lSuccess = testSection.SetPhysicalComponentValueInDirection(lComponent, eDirection.East);

      Assert.AreEqual(false, lSuccess);
      Assert.IsNull(testSection.Right);
    }

    [Test]
    public void SettingPhysicalComponentByDirection_WhereDirectionDoesntExistAtAll_Fails()
    {
      var lComponent = new TestComponent { Value = "Test5" };
      var lSuccess = testSection.SetPhysicalComponentValueInDirection(lComponent, eDirection.West);

      Assert.AreEqual(false, lSuccess);
    }
  }

  internal class TestSection : IComponentSection
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

  internal class TestComponent : IComponent
  {
    public string Value;

    public eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}
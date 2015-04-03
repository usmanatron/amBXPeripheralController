using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

      Assert.AreEqual(testSection.GetComponentValueInDirection(eDirection.North), component);
    }

    [Test]
    public void GettingComponentByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var northEast = testSection.GetComponentValueInDirection(eDirection.NorthEast);
      var east = testSection.GetComponentValueInDirection(eDirection.East);
      var southEast = testSection.GetComponentValueInDirection(eDirection.SouthEast);

      Assert.AreEqual(testSection.GetComponentValueInDirection(eDirection.NorthEast), northEast);
      Assert.AreEqual(testSection.GetComponentValueInDirection(eDirection.East), east);
      Assert.AreEqual(testSection.GetComponentValueInDirection(eDirection.SouthEast), southEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      Assert.Throws<InvalidOperationException>(() => testSection.GetComponentValueInDirection(eDirection.South));
    }
  }

  internal class TestSection : IComponentSection
  {
#pragma warning disable 169 // Fields are used exclusively by reflection

    public List<TestComponent> Components;

    public IEnumerable<IDirectionalComponent> GetComponents()
    {
      foreach (var component in Components)
      {
        yield return (IDirectionalComponent)component;
      }
    }

#pragma warning restore 169
  }

  internal class TestComponent : IDirectionalComponent
  {
    public string Value;

    public override eComponentType ComponentType()
    {
      return eComponentType.Light;
    }
  }
}
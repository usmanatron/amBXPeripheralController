using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace aPC.Common.Tests.Entities
{
  [TestFixture]
  internal class EntityExtensionTests
  {
    private TestSection testSection;

    [SetUp]
    public void Setup()
    {
      this.testSection = new TestSection();
      testSection.Components = new List<TestComponent>
      {
        new TestComponent() { Direction = eDirection.North, Value = "Value-North"},
        new TestComponent() { Direction = eDirection.South, Value = "Value-South1"},
        new TestComponent() { Direction = eDirection.South, Value = "Value-South2"}
      };
    }

    [Test]
    public void GettingComponentByDirection_GivesCorrectMember()
    {
      var component = testSection.GetComponentSectionInDirection(eDirection.North);

      Assert.AreEqual(testSection.GetComponentSectionInDirection(eDirection.North), component);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      Assert.Throws<InvalidOperationException>(() => testSection.GetComponentSectionInDirection(eDirection.South));
    }
  }

  internal class TestSection : IComponentSection
  {
    public List<TestComponent> Components;

    public IEnumerable<DirectionalComponent> GetComponents()
    {
      foreach (var component in Components)
      {
        yield return (DirectionalComponent)component;
      }
    }
  }

  internal class TestComponent : DirectionalComponent
  {
    public string Value;

    public TestComponent()
      : base(eComponentType.Light)
    {
    }

    public object Clone()
    {
      throw new NotImplementedException();
    }

    public override bool Equals(object other)
    {
      throw new NotImplementedException();
    }
  }
}
using System;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  class SectionBuilderBaseTests
  {
    [Test]
    public void FindingByDirection_GivesCorrectMember()
    {
      var lObject = new TestSection();
      var lField = lObject.GetComponentValueInDirection(eDirection.North);

      Assert.AreEqual(lObject.Up, lField);
    }

    [Test]
    public void FindingByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var lObject = new TestSection();
      var lNorthEast = lObject.GetComponentValueInDirection(eDirection.NorthEast);
      var lEast = lObject.GetComponentValueInDirection(eDirection.East);
      var lSouthEast = lObject.GetComponentValueInDirection(eDirection.SouthEast);

      Assert.AreEqual(lObject.Right, lNorthEast);
      Assert.AreEqual(lObject.Right, lEast);
      Assert.AreEqual(lObject.Right, lSouthEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      var lObject = new TestSection();
      Assert.Throws<InvalidOperationException>(() => lObject.GetComponentValueInDirection(eDirection.South));
    }

    /* 
     * Fixture Setup - Some Physical ones
     * Show that the other method works against this
     * 
     * Check FadeTime correctly saved on a SectionBase 
     * 
     * Test the Set methods!
     */

  }
  class TestSection : SectionBase<TestComponent>
  {
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
  }

  class TestComponent : Component
  {
  }
}

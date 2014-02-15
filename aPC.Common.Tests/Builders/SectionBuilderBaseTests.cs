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
      var lObject = new SingleDirectionTest();
      var lField = lObject.GetComponentInfoInDirection(eDirection.North);

      Assert.AreEqual(lObject.GetType().GetField("Up"), lField);
    }

    [Test]
    public void FindingByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var lObject = new SingleDirectionTest();
      var lNorthEast = lObject.GetComponentInfoInDirection(eDirection.NorthEast);
      var lEast = lObject.GetComponentInfoInDirection(eDirection.East);
      var lSouthEast = lObject.GetComponentInfoInDirection(eDirection.SouthEast);

      Assert.AreEqual(lObject.GetType().GetField("Right"), lNorthEast);
      Assert.AreEqual(lObject.GetType().GetField("Right"), lEast);
      Assert.AreEqual(lObject.GetType().GetField("Right"), lSouthEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      var lObject = new SingleDirectionTest();
      Assert.Throws<InvalidOperationException>(() => lObject.GetComponentInfoInDirection(eDirection.South));
    }

    /* 
     * Fixture Setup - Some Physical ones
     * Show that the other method works against this
     * 
     * Check FadeTime correctly saved on a SectionBase 
     */

  }
  class SingleDirectionTest : SectionBase<TestComponent>
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
    public override eComponentType ComponentType()
    {
      throw new NotImplementedException();
    }
  }
}

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
      var lBuilder = new SectionBuilderBase();
      var lObject = new SingleDirectionTest();
      var lField = lBuilder.GetComponentInfoInDirection(lObject, eDirection.North);

      Assert.AreEqual(lObject.GetType().GetField("Up"), lField);
    }

    [Test]
    public void FindingByDirection_WhereFieldHasMultipleDifferentDirections_GivesCorrectMember()
    {
      var lBuilder = new SectionBuilderBase();
      var lObject = new SingleDirectionTest();
      var lNorthEast = lBuilder.GetComponentInfoInDirection(lObject, eDirection.NorthEast);
      var lEast = lBuilder.GetComponentInfoInDirection(lObject, eDirection.East);
      var lSouthEast = lBuilder.GetComponentInfoInDirection(lObject, eDirection.SouthEast);

      Assert.AreEqual(lObject.GetType().GetField("Right"), lNorthEast);
      Assert.AreEqual(lObject.GetType().GetField("Right"), lEast);
      Assert.AreEqual(lObject.GetType().GetField("Right"), lSouthEast);
    }

    [Test]
    public void MultipleFieldsSharingTheSameDirection_ThrowsException()
    {
      var lBuilder = new SectionBuilderBase();
      var lObject = new SingleDirectionTest();
      Assert.Throws<InvalidOperationException>(() => lBuilder.GetComponentInfoInDirection(lObject, eDirection.South));
    }

    /* 
     * Fixture Setup - Some Physical ones
     * Show that the other method works against this
     * 
     * Check FadeTime correctly saved on a SectionBase 
     */

    class SingleDirectionTest : SectionBase
    {
#pragma warning disable 169 // Fields are used exclusively by reflection
      [Direction(eDirection.North)]
      public int Up;

      [Direction(eDirection.NorthEast)]
      [Direction(eDirection.East)]
      [Direction(eDirection.SouthEast)]
      public string Right;

      [Direction(eDirection.South)]
      public bool Down;

      [Direction(eDirection.South)]
      public bool DownClone;
#pragma warning restore 169
    }

  }

}

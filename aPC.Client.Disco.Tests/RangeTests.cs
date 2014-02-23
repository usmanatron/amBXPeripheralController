using System;
using NUnit.Framework;
using aPC.Client.Disco;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  class RangeTests
  {
    [Test]
    [TestCase(0, 1f, 1f)]
    [TestCase(0.5f, 2f, 1.5f)]
    [TestCase(-0.5f, 0.5f, 1f)]
    [TestCase(-2, -0.5f, 1.5f)]
    public void GivenSpecificInputs_ReturnsExpectedWidth(float xiMinimum, float xiMaximum, float xiWidth)
    {
      var lRange = new Range(xiMinimum, xiMaximum);
      Assert.AreEqual(xiWidth, lRange.Width);
    }

    [Test]
    public void ConstructorValuesInDescendingOrder_ThrowsException()
    {
      Assert.Throws<ArgumentException>(() => new Range(1f, 0f));
    }

    [Test]
    [TestCase(0f, 1f, 0.5d, 0.5f)]
    [TestCase(0f, 4f, 0.25d, 1f)]
    [TestCase(-2f, 2f, 0.5d, 0f)]
    [TestCase(1f, 2f, 0.75d, 1.75f)]
    [TestCase(-5f, -3f, 0.25d, -4.5f)]
    public void SpecificInputs_GiveExpectedScaledValue(float xiMinimum, float xiMaximum, double xiValue, float xiScaledValue)
    {
      var lRange = new Range(xiMinimum, xiMaximum);
      Assert.AreEqual(xiScaledValue, lRange.GetScaledValue(xiValue));
    }

    [Test]
    [TestCase(-0.5d, 2)]
    [TestCase(3d, 4)]
    public void OutOfRangeInputToGetScaledValue_ReturnsClippedScaledValue(double xiValue, float xiScaledValue)
    {
      var lRange = GetStandardRange();
      Assert.AreEqual(xiScaledValue, lRange.GetScaledValue(xiValue));
    }

    [Test]
    public void RangeIsNotEqualToNull()
    {
      var lRange = GetStandardRange();
      Assert.AreEqual(false, lRange.Equals(null));
    }
    
    [Test]
    [TestCase(2, 4, 3, 5)]
    [TestCase(2, 4, 1, 4)]
    [TestCase(0, 0, -1, 0)]
    public void GetHashCode_WorksAsExpected(int xiMin1, int xiMax1, int xiMin2, int xiMax2)
    {
      var lFirstRange = new Range(xiMin1, xiMax1);
      var lSecondRange = new Range(xiMin2, xiMax2);

      Assert.AreNotEqual(lFirstRange.GetHashCode(), lSecondRange.GetHashCode());
    }

    private Range GetStandardRange()
    {
      return new Range(2, 4);
    }
  }
}
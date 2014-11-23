using NUnit.Framework;
using System;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  internal class RangeTests
  {
    [Test]
    [TestCase(0, 1f, 1f)]
    [TestCase(0.5f, 2f, 1.5f)]
    [TestCase(-0.5f, 0.5f, 1f)]
    [TestCase(-2, -0.5f, 1.5f)]
    public void GivenSpecificInputs_ReturnsExpectedWidth(float minimum, float maximum, float width)
    {
      var range = new Range(minimum, maximum);
      Assert.AreEqual(width, range.Width);
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
    public void SpecificInputs_GiveExpectedScaledValue(float minimum, float maximum, double value, float scaledValue)
    {
      var range = new Range(minimum, maximum);
      Assert.AreEqual(scaledValue, range.GetScaledValue(value));
    }

    [Test]
    [TestCase(-0.5d, 2)]
    [TestCase(3d, 4)]
    public void OutOfRangeInputToGetScaledValue_ReturnsClippedScaledValue(double value, float scaledValue)
    {
      var range = GetStandardRange();
      Assert.AreEqual(scaledValue, range.GetScaledValue(value));
    }

    [Test]
    public void RangeIsNotEqualToNull()
    {
      var range = GetStandardRange();
      Assert.AreEqual(false, range.Equals(null));
    }

    [Test]
    [TestCase(2, 4, 3, 5)]
    [TestCase(2, 4, 1, 4)]
    [TestCase(0, 0, -1, 0)]
    public void GetHashCode_WorksAsExpected(int xiMin1, int xiMax1, int xiMin2, int xiMax2)
    {
      var firstRange = new Range(xiMin1, xiMax1);
      var secondRange = new Range(xiMin2, xiMax2);

      Assert.AreNotEqual(firstRange.GetHashCode(), secondRange.GetHashCode());
    }

    private Range GetStandardRange()
    {
      return new Range(2, 4);
    }
  }
}
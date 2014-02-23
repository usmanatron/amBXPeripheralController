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
    
/* qqUMI
 * Need to write a load of tests:
 * Settings can't be tested (and doesn't need to be because it's just a data structure), but I should create a test 
 * Settings thing with specific data hard-coded.  Maybe just the defaults?
 * 
 * DiscoTask could be tested by injecting mock scene generator and notification service - check that if we run it for 1 second
 * then the number of scenes pushed to the notification service is within some tolerance, and that they are all the same scenes
 * returned by the generator.
 * 
 * Finish Argumentreader - check that specific arguments work by passing one in and making sure it no longer is
 * equal to the default in Settings.
 * NotificationService - Mock out the communication part?  Just check it serialises and passes the string I guess>
 * 
 * RandomSceneGenerator - check it builds a valid scene => serialisable?  Could mock out systems Random to make it always 
 * return 0.5 (say), as I can test against that then!
 * 
 * RandomLSGenerator - same as above.  Probably mock out Random and Settings and confirm it creates something I expect?
 * 
 */
  }
}
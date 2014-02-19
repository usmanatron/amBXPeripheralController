using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests
{
  [TestFixture]
  public class AtypicalFirstRunInfiniteTickerTests
  {
    [Test]
    [TestCase(-5, 6)]
    [TestCase(0, 6)]
    [TestCase(5, -5)]
    [TestCase(5, 0)]
    public void NonPositiveArguments_ThrowsException(int xiFirstRun, int xiSecondRun)
    {
      Assert.Throws<InvalidOperationException>(() => CreateTicker(xiFirstRun, xiSecondRun));
    }

    [Test]
    public void NewTicker_StartsAtZero()
    {
      var lTicker = CreateDefaultTicker();
      Assert.AreEqual(0, lTicker.Index);
    }

    [Test]
    public void NewTicker_HasFirstRunTrue()
    {
      var lTicker = CreateDefaultTicker();
      Assert.AreEqual(true, lTicker.IsFirstRun);
    }

    [Test]
    public void NewTicker_TickedOnce_IncreasesByOne()
    {
      var lTicker = CreateDefaultTicker();
      lTicker.Advance();
      Assert.AreEqual(1, lTicker.Index);
    }

    [Test]
    public void NewTicker_AdvancingThroughFirstRun_ReturnsToZero()
    {
      var lTicker = CreateDefaultTicker();

      for (int lTicks = 0; lTicks < mDefaultFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(0, lTicker.Index);
    }

    [Test]
    public void NewTicker_AdvancingThroughFirstRun_ChangesFirstRunFalse()
    {
      var lTicker = CreateDefaultTicker();

      for (int lTicks = 0; lTicks < mDefaultFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(false, lTicker.IsFirstRun);
    }

    [Test]
    public void NewTickerWithDifferentSubsequentSize_AdvancingThroughTwoFullRuns_ReturnsToZero()
    {
      var lTicker = CreateDefaultTicker();

      // The following should complete two full runs: One at the smaller initial size and one at the larger subsequent one.
      for (int lTicks = 0; lTicks < mDefaultFirstRunTickerSize + mDefaultSubsequentTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(0, lTicker.Index);
    }

    private AtypicalFirstRunInfiniteTicker CreateTicker(int xiFirstRun, int xiSubsequentRun)
    {
      return new AtypicalFirstRunInfiniteTicker(xiFirstRun, xiSubsequentRun);
    }

    private AtypicalFirstRunInfiniteTicker CreateDefaultTicker()
    {
      return CreateTicker(mDefaultFirstRunTickerSize, mDefaultSubsequentTickerSize);
    }

    // Note: the tests above assume these to be non equal
    private const int mDefaultFirstRunTickerSize = 3;
    private const int mDefaultSubsequentTickerSize = 7;
  }
}

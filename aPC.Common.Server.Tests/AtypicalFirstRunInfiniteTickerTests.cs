using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests
{
  [TestFixture]
  public class AtypicalFirstRunInfiniteTickerTests
  {
    [Test]
    public void NonPositiveArgumentsDisallowed()
    {
      // Invalid first value
      Assert.Throws<InvalidOperationException>(() => CreateTicker(-5, 6));
      Assert.Throws<InvalidOperationException>(() => CreateTicker(0, 6));
      
      // Invalid subsequent value
      Assert.Throws<InvalidOperationException>(() => CreateTicker(5, -5));
      Assert.Throws<InvalidOperationException>(() => CreateTicker(5, 0));

    }

    [Test]
    public void IndexIsZeroBased()
    {
      var lTicker = CreateDefaultTicker();
      Assert.AreEqual(0, lTicker.Index);
    }

    [Test]
    public void NewTickerHasFirstRunTrue()
    {
      var lTicker = CreateDefaultTicker();
      Assert.AreEqual(true, lTicker.IsFirstRun);
    }

    [Test]
    public void TickingIncreasesByOne()
    {
      var lTicker = CreateDefaultTicker();
      lTicker.Advance();
      Assert.AreEqual(1, lTicker.Index);
    }

    [Test]
    public void AdvancingThroughFirstRunReturnsToZero()
    {
      var lTicker = CreateDefaultTicker();

      for (int lTicks = 0; lTicks < mDefaultFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(0, lTicker.Index);
    }

    [Test]
    public void AdvancingThroughFirstRunChangesFirstRunFalse()
    {
      var lTicker = CreateDefaultTicker();

      for (int lTicks = 0; lTicks < mDefaultFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(false, lTicker.IsFirstRun);
    }

    [Test]
    public void SecondRunHasDifferentSize()
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

using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests
{
  [TestFixture]
  public class AtypicalFirstRunInfiniteTickerTests
  {
    [Test]
    [TestCase(-5, 3)]
    [TestCase(0, 3)]
    public void NonPositiveInitialCount_ThrowsException(int xiInitial, int xiSubsequent)
    {
      Assert.Throws<InvalidOperationException>(() => CreateTicker(xiInitial, xiSubsequent));
    }

    [Test]
    [TestCase(7, -5)]
    [TestCase(7, -1)]
    public void NegativeSubsequentCount_ThrowsException(int xiInitial, int xiSubsequent)
    {
      Assert.Throws<InvalidOperationException>(() => CreateTicker(xiInitial, xiSubsequent));
    }

    [Test]
    public void NewTicker_WithZeroSubsequentCount_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => CreateTicker(5, 0));
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
      var lTicker = CreateTicker(ExampleFirstRunTickerSize);

      for (int lTicks = 0; lTicks < ExampleFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(0, lTicker.Index);
    }

    [Test]
    public void NewTicker_AdvancingThroughFirstRun_ChangesFirstRunFalse()
    {
      var lTicker = CreateTicker(ExampleFirstRunTickerSize);

      for (int lTicks = 0; lTicks < ExampleFirstRunTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(false, lTicker.IsFirstRun);
    }

    [Test]
    public void NewTickerWithDifferentSubsequentSize_AdvancingThroughTwoFullRuns_ReturnsToZero()
    {
      var lTicker = CreateTicker(ExampleFirstRunTickerSize, ExampleSubsequentTickerSize);

      // The following should complete two full runs: One at the smaller initial size and one at the larger subsequent one.
      for (int lTicks = 0; lTicks < ExampleFirstRunTickerSize + ExampleSubsequentTickerSize; lTicks++)
      {
        lTicker.Advance();
      }

      Assert.AreEqual(0, lTicker.Index);
    }

    private AtypicalFirstRunInfiniteTicker CreateTicker(int xiFirstRun, int xiSubsequentRun = 42)
    {
      return new AtypicalFirstRunInfiniteTicker(xiFirstRun, xiSubsequentRun);
    }

    private AtypicalFirstRunInfiniteTicker CreateDefaultTicker()
    {
      return CreateTicker(42);
    }

    // Note: the tests above assume these to be non equal
    private const int ExampleFirstRunTickerSize = 3;
    private const int ExampleSubsequentTickerSize = 7;
  }
}

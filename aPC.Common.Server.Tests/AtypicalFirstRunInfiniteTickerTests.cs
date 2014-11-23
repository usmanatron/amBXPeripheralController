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
    public void NonPositiveInitialCount_ThrowsException(int initial, int subsequent)
    {
      Assert.Throws<InvalidOperationException>(() => CreateTicker(initial, subsequent));
    }

    [Test]
    [TestCase(7, -5)]
    [TestCase(7, -1)]
    public void NegativeSubsequentCount_ThrowsException(int initial, int subsequent)
    {
      Assert.Throws<InvalidOperationException>(() => CreateTicker(initial, subsequent));
    }

    [Test]
    public void NewTicker_WithZeroSubsequentCount_DoesNotThrow()
    {
      Assert.DoesNotThrow(() => CreateTicker(5, 0));
    }

    [Test]
    public void NewTicker_StartsAtZero()
    {
      var ticker = CreateDefaultTicker();
      Assert.AreEqual(0, ticker.Index);
    }

    [Test]
    public void NewTicker_HasFirstRunTrue()
    {
      var ticker = CreateDefaultTicker();
      Assert.AreEqual(true, ticker.IsFirstRun);
    }

    [Test]
    public void NewTicker_TickedOnce_IncreasesByOne()
    {
      var ticker = CreateDefaultTicker();
      ticker.Advance();
      Assert.AreEqual(1, ticker.Index);
    }

    [Test]
    public void NewTicker_AdvancingThroughFirstRun_ReturnsToZero()
    {
      var ticker = CreateTicker(ExampleFirstRunTickerSize);

      for (int ticks = 0; ticks < ExampleFirstRunTickerSize; ticks++)
      {
        ticker.Advance();
      }

      Assert.AreEqual(0, ticker.Index);
    }

    [Test]
    public void NewTicker_AdvancingThroughFirstRun_ChangesFirstRunFalse()
    {
      var ticker = CreateTicker(ExampleFirstRunTickerSize);

      for (int ticks = 0; ticks < ExampleFirstRunTickerSize; ticks++)
      {
        ticker.Advance();
      }

      Assert.AreEqual(false, ticker.IsFirstRun);
    }

    [Test]
    public void NewTickerWithDifferentSubsequentSize_AdvancingThroughTwoFullRuns_ReturnsToZero()
    {
      var ticker = CreateTicker(ExampleFirstRunTickerSize, ExampleSubsequentTickerSize);

      // The following should complete two full runs: One at the smaller initial size and one at the larger subsequent one.
      for (int ticks = 0; ticks < ExampleFirstRunTickerSize + ExampleSubsequentTickerSize; ticks++)
      {
        ticker.Advance();
      }

      Assert.AreEqual(0, ticker.Index);
    }

    [Test]
    public void RefreshTicker_ResetsIndex()
    {
      var ticker = CreateTicker(2, 1);
      ticker.Advance();

      ticker.Refresh();

      Assert.AreEqual(0, ticker.Index);
    }

    [Test]
    public void RefreshTicker_SetsBackToFirstRun()
    {
      var ticker = CreateTicker(2, 1);
      ticker.Advance();
      ticker.Advance();

      Assert.IsFalse(ticker.IsFirstRun);

      ticker.Refresh();

      Assert.IsTrue(ticker.IsFirstRun);
    }

    private AtypicalFirstRunInfiniteTicker CreateTicker(int xiFirstRun, int subsequentRun = 42)
    {
      return new AtypicalFirstRunInfiniteTicker(xiFirstRun, subsequentRun);
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
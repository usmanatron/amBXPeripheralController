using System;

namespace aPC.Client.Disco.Tests
{
  class TestRandom : Random
  {
    private readonly double mRandomNumber;

    public TestRandom(double xiRandomNumber)
    {
      mRandomNumber = xiRandomNumber;
    }

    public override double NextDouble()
    {
      return mRandomNumber;
    }
  }
}

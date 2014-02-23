using System;

namespace aPC.Client.Disco.Tests
{
  class TestRandom : Random
  {
    public TestRandom(double xiRandomNumber)
    {
      mRandomNumber = xiRandomNumber;
    }

    public override double NextDouble()
    {
      return mRandomNumber;
    }

    private readonly double mRandomNumber;
  }
}

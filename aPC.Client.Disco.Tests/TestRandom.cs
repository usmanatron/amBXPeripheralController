using System;

namespace aPC.Client.Disco.Tests
{
  internal class TestRandom : Random
  {
    private readonly double randomNumber;

    public TestRandom(double randomNumber)
    {
      this.randomNumber = randomNumber;
    }

    public override double NextDouble()
    {
      return randomNumber;
    }
  }
}
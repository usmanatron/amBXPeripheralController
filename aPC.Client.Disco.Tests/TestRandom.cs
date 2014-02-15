using System;

namespace aPC.Client.Disco.Tests
{
  class TestRandom : Random
  {
    public override double NextDouble()
    {
      // Specifically selected to ensure that its less than the default Change threshold.
      return 0.25;
    }
  }
}

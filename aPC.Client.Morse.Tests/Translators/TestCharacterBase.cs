using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal abstract class TestCharacterBase
  {
    public TestCharacterBase(List<IMorseBlock> xiExpectedCode)
    {
      ExpectedCode = xiExpectedCode;
    }

    public int ExpectedCodeCount
    {
      get
      {
        return ExpectedCode.Count;
      }
    }

    public readonly List<IMorseBlock> ExpectedCode;
  }
}

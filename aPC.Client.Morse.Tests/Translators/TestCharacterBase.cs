using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal abstract class TestCharacterBase
  {
    public readonly List<IMorseBlock> ExpectedCode;

    protected TestCharacterBase(List<IMorseBlock> expectedCode)
    {
      ExpectedCode = expectedCode;
    }

    public int ExpectedCodeCount
    {
      get
      {
        return ExpectedCode.Count;
      }
    }
  }
}
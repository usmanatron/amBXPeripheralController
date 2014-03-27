using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  class TestMultiCharacterData : TestCharacterBase
  {
    public TestMultiCharacterData(string xiWord, List<IMorseBlock> xiExpectedCode) : base(xiExpectedCode)
    {
      Word = xiWord;
    }

    public override string ToString()
    {
      return Word;
    }

    public readonly string Word;
  }
}

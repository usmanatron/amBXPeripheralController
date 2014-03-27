using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  class TestWordData
  {
    public TestWordData(string xiWord, List<IMorseBlock> xiExpectedCode)
    {
      Word = xiWord;
      ExpectedCode = xiExpectedCode;
    }

    public int ExpectedCodeCount
    {
      get
      {
        return ExpectedCode.Count;
      }
    }

    public override string ToString()
    {
      return Word;
    }

    public readonly string Word;
    public readonly IList<IMorseBlock> ExpectedCode;
  }
}

using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Codes
{
  class TestCharacterData
  {
    public TestCharacterData(char xiCharacter, List<IMorseBlock> xiExpectedCode)
    {
      Character = xiCharacter;
      ExpectedCode = xiExpectedCode;
    }

    public readonly char Character;
    public readonly List<IMorseBlock> ExpectedCode;
    
    public int ExpectedCodeCount
    {
      get
      {
        return ExpectedCode.Count;
      }
    }
  }
}

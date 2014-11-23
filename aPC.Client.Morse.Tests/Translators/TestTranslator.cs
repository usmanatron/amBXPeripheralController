using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestTranslator : TranslatorBase
  {
    public override List<IMorseBlock> Translate()
    {
      throw new NotImplementedException();
    }

    public List<IMorseBlock> SeparateList(List<IMorseBlock> xiList, IMorseBlock xiSeparator)
    {
      return AddSeparatorsToList(xiList, xiSeparator);
    }
  }
}
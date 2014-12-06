using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestTranslator : TranslatorBase
  {
    public override List<IMorseBlock> Translate(string content)
    {
      throw new NotImplementedException();
    }

    public override IMorseBlock Separator
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public List<IMorseBlock> SeparateList(List<IMorseBlock> list, IMorseBlock separator)
    {
      return AddSeparatorsToList(list, separator);
    }
  }
}
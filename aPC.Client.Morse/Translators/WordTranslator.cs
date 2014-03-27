using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public class WordTranslator : TranslatorBase
  {
    public WordTranslator(string xiWord)
    {
      if (xiWord.Contains(' '))
      {
        throw new InvalidOperationException("A space was found in the following word to be translated, |" + xiWord + "|, which is not supported.");
      }

      mWord = xiWord;
    }

    public override List<IMorseBlock> Translate()
    {
      var lTranslatedWord = new List<List<IMorseBlock>>();

      foreach(var lCharacter in mWord.ToCharArray())
      {
        lTranslatedWord.Add(new CharacterTranslator(lCharacter).Translate());
      }

      return AddSeparatorsToList(lTranslatedWord, new CharacterSeparator());
    }

    private string mWord;
  }
}

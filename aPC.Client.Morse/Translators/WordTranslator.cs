using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public class WordTranslator : TranslatorBase
  {
    private string word;

    public WordTranslator(string word)
    {
      if (word.Contains(' '))
      {
        throw new InvalidOperationException("A space was found in the following word to be translated, |" + word + "|, which is not supported.");
      }

      this.word = word;
    }

    public override List<IMorseBlock> Translate()
    {
      var translatedWord = new List<List<IMorseBlock>>();

      foreach (var character in word.ToCharArray())
      {
        translatedWord.Add(new CharacterTranslator(character).Translate());
      }

      return AddSeparatorsToList(translatedWord, new CharacterSeparator());
    }
  }
}
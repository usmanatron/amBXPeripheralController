using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public class WordTranslator : TranslatorBase
  {
    private CharacterTranslator baseTranslator;

    public WordTranslator(CharacterTranslator baseTranslator)
    {
      this.baseTranslator = baseTranslator;
    }

    public override List<IMorseBlock> Translate(string content)
    {
      ThrowIfInputInvalid(content);

      var translatedWord = new List<List<IMorseBlock>>();

      foreach (var character in content.ToCharArray())
      {
        translatedWord.Add(baseTranslator.Translate(character.ToString()));
      }

      return AddSeparatorsToList(translatedWord, Separator);
    }

    private void ThrowIfInputInvalid(string word)
    {
      if (word.Contains(' '))
      {
        throw new InvalidOperationException("A space was found in the following word to be translated: |" + word + "|.  This should never happen!");
      }
    }

    public override IMorseBlock Separator
    {
      get
      {
        return new CharacterSeparator();
      }
    }
  }
}
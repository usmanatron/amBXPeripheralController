using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public class CharacterTranslator : TranslatorBase
  {
    private static IMorseBlock dot = new Dot();
    private static IMorseBlock dash = new Dash();

    public override List<IMorseBlock> Translate(string content)
    {
      var character = ConvertToCharacter(content);

      var rawCharacter = RawCharacters[Char.ToLower(character)];
      return AddSeparatorsToList(rawCharacter, Separator);
    }

    private char ConvertToCharacter(string content)
    {
      if (content.Length != 1)
      {
        throw new InvalidOperationException("The content given to the CharacterTranslator is longer than one character: |" + content + "|.  This should never happen!");
      }

      return content.ToCharArray().Single();
    }

    public override IMorseBlock Separator
    {
      get
      {
        return new DotDashSeparator();
      }
    }

    /// <summary>
    ///   Gives the Morse Code equivalent of every available character
    ///   *WITHOUT THE SEPARATORS* between dots and dashes.
    /// </summary>
    private static Dictionary<char, List<IMorseBlock>> RawCharacters = new Dictionary<char, List<IMorseBlock>>
    {
      {'a', new List<IMorseBlock>(){dot, dash}},
      {'b', new List<IMorseBlock>(){dash, dot, dot, dot}},
      {'c', new List<IMorseBlock>(){dash, dot, dash, dot}},
      {'d', new List<IMorseBlock>(){dash, dot, dot}},
      {'e', new List<IMorseBlock>(){dot}},
      {'f', new List<IMorseBlock>(){dot, dot, dash, dot}},
      {'g', new List<IMorseBlock>(){dash, dash, dot}},
      {'h', new List<IMorseBlock>(){dot, dot, dot, dot}},
      {'i', new List<IMorseBlock>(){dot, dot}},
      {'j', new List<IMorseBlock>(){dot, dash, dash, dash}},
      {'k', new List<IMorseBlock>(){dash, dot, dash}},
      {'l', new List<IMorseBlock>(){dot, dash, dot, dot}},
      {'m', new List<IMorseBlock>(){dash, dash}},
      {'n', new List<IMorseBlock>(){dash, dot}},
      {'o', new List<IMorseBlock>(){dash, dash, dash}},
      {'p', new List<IMorseBlock>(){dot, dash, dash, dot}},
      {'q', new List<IMorseBlock>(){dash, dash, dot, dash}},
      {'r', new List<IMorseBlock>(){dot, dash, dot}},
      {'s', new List<IMorseBlock>(){dot, dot, dot}},
      {'t', new List<IMorseBlock>(){dash}},
      {'u', new List<IMorseBlock>(){dot, dot, dash}},
      {'v', new List<IMorseBlock>(){dot, dot, dot, dash}},
      {'w', new List<IMorseBlock>(){dot, dash, dash}},
      {'x', new List<IMorseBlock>(){dash, dot, dot, dash}},
      {'y', new List<IMorseBlock>(){dash, dot, dash, dash}},
      {'z', new List<IMorseBlock>(){dash, dash, dot, dot}},

      {'0', new List<IMorseBlock>(){dash, dash, dash, dash, dash}},
      {'1', new List<IMorseBlock>(){dot, dash, dash, dash, dash}},
      {'2', new List<IMorseBlock>(){dot, dot, dash, dash, dash}},
      {'3', new List<IMorseBlock>(){dot, dot, dot, dash, dash}},
      {'4', new List<IMorseBlock>(){dot, dot, dot, dot, dash}},
      {'5', new List<IMorseBlock>(){dot, dot, dot, dot, dot}},
      {'6', new List<IMorseBlock>(){dash, dot, dot, dot, dot}},
      {'7', new List<IMorseBlock>(){dash, dash, dot, dot, dot}},
      {'8', new List<IMorseBlock>(){dash, dash, dash, dot, dot}},
      {'9', new List<IMorseBlock>(){dash, dash, dash, dash, dot}},

      {'.', new List<IMorseBlock>(){dot, dash, dot, dash, dot, dash}},
      {',', new List<IMorseBlock>(){dash, dash, dot, dot, dash, dash}},
      {'?', new List<IMorseBlock>(){dot, dot, dash, dash, dot, dot}},
      {'\'', new List<IMorseBlock>(){dot, dash, dash, dash, dash, dot}},
      {'!', new List<IMorseBlock>(){dash, dot, dash, dot, dash, dash}},
      {'/', new List<IMorseBlock>(){dash, dot, dot, dash, dot}},
      {'(', new List<IMorseBlock>(){dash, dot, dash, dash, dot}},
      {')', new List<IMorseBlock>(){dash, dot, dash, dash, dot, dash}},
      {'&', new List<IMorseBlock>(){dot, dash, dot, dot, dot}},
      {':', new List<IMorseBlock>(){dash, dash, dash, dot, dot, dot}},
      {';', new List<IMorseBlock>(){dash, dot, dash, dot, dash, dot}},
      {'=', new List<IMorseBlock>(){dash, dot, dot, dot, dash}},
      {'+', new List<IMorseBlock>(){dot, dash, dot, dash, dot}},
      {'-', new List<IMorseBlock>(){dash, dot, dot, dot, dot, dash}},
      {'_', new List<IMorseBlock>(){dot, dot, dash, dash, dot, dash}},
      {'\"', new List<IMorseBlock>(){dot, dash, dot, dot, dash, dot}},
      {'$', new List<IMorseBlock>(){dot, dot, dot, dash, dot, dot, dash}},
      {'@', new List<IMorseBlock>(){dot, dash, dash, dot, dash, dot}},
    };
  }
}
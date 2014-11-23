using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public class CharacterTranslator : TranslatorBase
  {
    public CharacterTranslator(char xiCharacter)
    {
      mCharacter = xiCharacter;
    }

    public override List<IMorseBlock> Translate()
    {
      var lSeparator = new DotDashSeparator();
      var lRawCharacter = RawCharacters[Char.ToLower(mCharacter)];
      return AddSeparatorsToList(lRawCharacter, lSeparator);
    }

    private char mCharacter;

    /// <summary>
    ///   Gives the Morse Code equivalent of every available character
    ///   * WITHOUT THE SEPARATORS* between dots and dashes.
    /// </summary>
    /// <remarks>
    ///   Would it be better to just have one actual instance of Dot
    ///   and Dash and just reuse it?  Something worth considering,
    ///   especially once the client runs end-to-end.
    /// </remarks>
    private static Dictionary<char, List<IMorseBlock>> RawCharacters = new Dictionary<char, List<IMorseBlock>>
    {
      {'a', new List<IMorseBlock>(){new Dot(), new Dash()}},
      {'b', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dot()}},
      {'c', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dot()}},
      {'d', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot()}},
      {'e', new List<IMorseBlock>(){new Dot()}},
      {'f', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash(), new Dot()}},
      {'g', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot()}},
      {'h', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dot()}},
      {'i', new List<IMorseBlock>(){new Dot(), new Dot()}},
      {'j', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dash()}},
      {'k', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash()}},
      {'l', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dot()}},
      {'m', new List<IMorseBlock>(){new Dash(), new Dash()}},
      {'n', new List<IMorseBlock>(){new Dash(), new Dot()}},
      {'o', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash()}},
      {'p', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dot()}},
      {'q', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dash()}},
      {'r', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot()}},
      {'s', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot()}},
      {'t', new List<IMorseBlock>(){new Dash()}},
      {'u', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash()}},
      {'v', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dash()}},
      {'w', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash()}},
      {'x', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dash()}},
      {'y', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dash()}},
      {'z', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dot()}},

      {'0', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash(), new Dash(), new Dash()}},
      {'1', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dash(), new Dash()}},
      {'2', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash(), new Dash(), new Dash()}},
      {'3', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dash(), new Dash()}},
      {'4', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dot(), new Dash()}},
      {'5', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dot(), new Dot()}},
      {'6', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dot(), new Dot()}},
      {'7', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dot(), new Dot()}},
      {'8', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash(), new Dot(), new Dot()}},
      {'9', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash(), new Dash(), new Dot()}},

      {'.', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dash(), new Dot(), new Dash()}},
      {',', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dot(), new Dash(), new Dash()}},
      {'?', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash(), new Dash(), new Dot(), new Dot()}},
      {'\'', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dash(), new Dash(), new Dot()}},
      {'!', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dot(), new Dash(), new Dash()}},
      {'/', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dash(), new Dot()}},
      {'(', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dash(), new Dot()}},
      {')', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dash(), new Dot(), new Dash()}},
      {'&', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dot(), new Dot()}},
      {':', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash(), new Dot(), new Dot(), new Dot()}},
      {';', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dot(), new Dash(), new Dot()}},
      {'=', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dot(), new Dash()}},
      {'+', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dash(), new Dot()}},
      {'-', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dot(), new Dot(), new Dash()}},
      {'_', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash(), new Dash(), new Dot(), new Dash()}},
      {'\"', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dot(), new Dash(), new Dot()}},
      {'$', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dash(), new Dot(), new Dot(), new Dash()}},
      {'@', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dot(), new Dash(), new Dot()}},
    };
  }
}
using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Morse.Codes
{
  public static class Characters
  {
    /// <summary>
    ///   Gives the Morse Code equivalent of every available character 
    ///   * WITHOUT THE SEPARATORS* between dots and dashes.
    /// </summary>
    public static Dictionary<char, List<IMorseBlock>> RawCharacters = new Dictionary<char, List<IMorseBlock>>
    {
      {'A', new List<IMorseBlock>(){new Dot(), new Dash()}},
      {'B', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dot()}},
      {'C', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dot()}},
      {'D', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot()}},
      {'E', new List<IMorseBlock>(){new Dot()}},
      {'F', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash(), new Dot()}},
      {'G', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot()}},
      {'H', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dot()}},
      {'I', new List<IMorseBlock>(){new Dot(), new Dot()}},
      {'J', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dash()}},
      {'K', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash()}},
      {'L', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot(), new Dot()}},
      {'M', new List<IMorseBlock>(){new Dash(), new Dash()}},
      {'N', new List<IMorseBlock>(){new Dash(), new Dot()}},
      {'O', new List<IMorseBlock>(){new Dash(), new Dash(), new Dash()}},
      {'P', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash(), new Dot()}},
      {'Q', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dash()}},
      {'R', new List<IMorseBlock>(){new Dot(), new Dash(), new Dot()}},
      {'S', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot()}},
      {'T', new List<IMorseBlock>(){new Dash()}},
      {'U', new List<IMorseBlock>(){new Dot(), new Dot(), new Dash()}},
      {'V', new List<IMorseBlock>(){new Dot(), new Dot(), new Dot(), new Dash()}},
      {'W', new List<IMorseBlock>(){new Dot(), new Dash(), new Dash()}},
      {'X', new List<IMorseBlock>(){new Dash(), new Dot(), new Dot(), new Dash()}},
      {'Y', new List<IMorseBlock>(){new Dash(), new Dot(), new Dash(), new Dash()}},
      {'Z', new List<IMorseBlock>(){new Dash(), new Dash(), new Dot(), new Dot()}},

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
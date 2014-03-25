using aPC.Client.Morse.Codes;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public abstract class TranslatorBase : ITranslator
  {

    public abstract List<IMorseBlock> Translate();

    /// <summary>
    ///   Adds an instance of the given type between every 
    ///   element in the list.  Does not add any new elements to the immediate start of end
    /// </summary>
    protected List<IMorseBlock> AddSeparatorsToList(List<IMorseBlock> xiList, IMorseBlock xiSeparator)
    {
      var lNewList = new List<IMorseBlock>();

      foreach (var lItem in xiList)
      {
        lNewList.Add(lItem);
        lNewList.Add(xiSeparator);
      }

      lNewList.RemoveAt(lNewList.Count - 1);
      return lNewList;
    }
  }
}

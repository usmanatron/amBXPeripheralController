using aPC.Client.Morse.Codes;
using System;
using System.Linq;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public abstract class TranslatorBase : ITranslator
  {
    public abstract List<IMorseBlock> Translate();

    /// <summary>
    ///   Adds an instance of the given type between every list of elements
    ///   in the list.  Does not add any new elements to the immediate start of end.
    /// </summary>
    protected List<IMorseBlock> AddSeparatorsToList(IEnumerable<List<IMorseBlock>> xiList, IMorseBlock xiSeparator)
    {
      var lNewList = new List<IMorseBlock>();

      foreach (var lItem in xiList)
      {
        lNewList.AddRange(lItem);
        lNewList.Add(xiSeparator);
      }

      lNewList.RemoveAt(lNewList.Count - 1);
      return lNewList;
    }


    /// <remarks>
    ///   Calls the method above, first breaking each item into a separate list
    ///   (i.e. we add a separator in between each element of xiList).
    /// </remarks>
    protected List<IMorseBlock> AddSeparatorsToList(List<IMorseBlock> xiList, IMorseBlock xiSeparator)
    {
      return AddSeparatorsToList(xiList.Select(item => new List<IMorseBlock> { item }), xiSeparator);
    }
  }
}

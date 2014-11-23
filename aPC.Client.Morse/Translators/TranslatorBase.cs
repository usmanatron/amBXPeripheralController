using aPC.Client.Morse.Codes;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public abstract class TranslatorBase : ITranslator
  {
    public abstract List<IMorseBlock> Translate();

    /// <summary>
    ///   Adds an instance of the given type between every list of elements
    ///   in the list.  Does not add any new elements to the immediate start of end.
    /// </summary>
    protected List<IMorseBlock> AddSeparatorsToList(IEnumerable<List<IMorseBlock>> list, IMorseBlock separator)
    {
      var newList = new List<IMorseBlock>();

      foreach (var item in list)
      {
        newList.AddRange(item);
        newList.Add(separator);
      }

      newList.RemoveAt(newList.Count - 1);
      return newList;
    }

    /// <remarks>
    ///   Calls the method above, first breaking each item into a separate list
    ///   (i.e. we add a separator in between each element of xiList).
    /// </remarks>
    protected List<IMorseBlock> AddSeparatorsToList(List<IMorseBlock> list, IMorseBlock separator)
    {
      return AddSeparatorsToList(list.Select(item => new List<IMorseBlock> { item }), separator);
    }
  }
}
using System.Collections.Generic;

namespace aPC.Client.Scene
{
  interface ISceneListing
  {

    void Reload();

    string BrowseItemName { get; }

    IEnumerable<string> DropdownListing { get; }

    string GetValue(string xiKey);
  }
}

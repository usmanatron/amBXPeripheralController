using System.Collections.Generic;

namespace aPC.Client.Scene
{
  interface ISceneListing
  {
    void Reload();

    string BrowseItemName { get; }

    Dictionary<string, string> Scenes { get; }
  }
}

﻿using System.Collections.Generic;

namespace aPC.Client.Scene
{
  interface ISceneListing
  {
    string BrowseItemName { get; }

    Dictionary<string, string> Scenes { get; }
  }
}
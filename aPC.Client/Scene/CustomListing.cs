using System.Collections.Generic;
using System.IO;

namespace aPC.Client.Scene
{
  class CustomListing : ISceneListing
  {
    public CustomListing()
    {
      LoadSavedScenes();
    }

    private void LoadSavedScenes()
    {
      Scenes = new Dictionary<string, string>();
      var lFiles = Directory.EnumerateFiles(ProfilesStore.Directory);

      foreach (var lFile in lFiles)
      {
        AddScene(ProfilesStore.GetFilenameWithoutExtension(lFile), File.ReadAllText(lFile));
      }

      // Finally add a "browse" choice to select you're own scene
      Scenes.Add(BrowseItemName, "");
    }
 
    public void AddScene(string xiKey, string xiValue)
    {
      Scenes.Add(xiKey, xiValue);
    }

    public IEnumerable<string> DropdownListing
    {
      get
      {
        return Scenes.Keys;
      }
    }

    public string GetValue(string xiKey)
    {
      return Scenes[xiKey];
    }

    public string BrowseItemName
    {
      get
      {
        return "<Browse...>";
      }
    }

    public Dictionary<string, string> Scenes { get; private set; }
  }
}
using System.Collections.Generic;
using System.IO;

namespace aPC.Client.Gui.Scene
{
  public class CustomListing : ISceneListing
  {
    public Dictionary<string, string> Scenes { get; private set; }

    public CustomListing()
    {
      LoadScenes();
    }

    public void Reload()
    {
      LoadScenes();
    }

    private void LoadScenes()
    {
      Scenes = new Dictionary<string, string>();

      if (!Directory.Exists(ProfilesStore.Directory))
      {
        Directory.CreateDirectory(ProfilesStore.Directory);
      }

      var files = Directory.EnumerateFiles(ProfilesStore.Directory);

      foreach (var file in files)
      {
        AddScene(ProfilesStore.GetFilenameWithoutExtension(file), File.ReadAllText(file));
      }

      // Finally add a "browse" choice to select you're own scene
      Scenes.Add(BrowseItemName, "");
    }

    public void AddScene(string key, string value)
    {
      Scenes.Add(key, value);
    }

    public IEnumerable<string> DropdownListing
    {
      get
      {
        return Scenes.Keys;
      }
    }

    public string GetValue(string key)
    {
      return Scenes[key];
    }

    public string BrowseItemName
    {
      get
      {
        return "<Add...>";
      }
    }
  }
}
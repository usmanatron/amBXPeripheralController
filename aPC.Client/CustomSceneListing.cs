using System;
using System.Collections.Generic;
using System.IO;

namespace aPC.Client
{
  class CustomSceneListing
  {
    public CustomSceneListing()
    {
      mBaseDirectory = Path.Combine(Environment.CurrentDirectory, "Profiles");
      mFileHandler = new CustomSceneFileHandler();

      LoadSavedScenes();
    }

    private void LoadSavedScenes()
    {
      Scenes = new Dictionary<string, string>();
      var lFiles = Directory.EnumerateFiles(mBaseDirectory);

      foreach (var lFile in lFiles)
      {
        Scenes.Add(StripPathAndExtension(lFile), File.ReadAllText(lFile));
      }

      // Finally add a "browse" choice to select you're own scene
      Scenes.Add(BrowseItemName, "");
    }

    private string StripPathAndExtension(string xiFullFilename)
    {
      var lFistCharAfterLastSlash = xiFullFilename.LastIndexOf(@"\", StringComparison.Ordinal) + 1;
      const int lXmlExtensionLength = 4;
      var lFilenameLength = xiFullFilename.Length - lFistCharAfterLastSlash - lXmlExtensionLength;
      return xiFullFilename.Substring(lFistCharAfterLastSlash, lFilenameLength);
    }



    private readonly string mBaseDirectory;
    private readonly CustomSceneFileHandler mFileHandler;

    public string BrowseItemName = "<Browse...>";
    public Dictionary<string, string> Scenes { get; private set; }
  }
}

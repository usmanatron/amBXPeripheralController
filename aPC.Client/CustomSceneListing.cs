using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace aPC.Client
{
  class CustomSceneListing
  {
    public CustomSceneListing()
    {
      mBaseDirectory = Path.Combine(Environment.CurrentDirectory, "Profiles");
      LoadSavedScenes();
    }

    private void LoadSavedScenes()
    {
      var lFiles = Directory.EnumerateFiles(mBaseDirectory);
      foreach (var lFile in lFiles)
      {
        Scenes.Add(lFile, File.ReadAllText(Path.Combine(mBaseDirectory, lFile)));
      }
    }

    // Includes path
    public void ImportScene(string xifilename)
    {
      throw new NotImplementedException();
    }

    public void DeleteScene(string xiFilename)
    {
      throw new NotImplementedException();
      //Scenes.Remove(xiFilename);
    }

    private string mBaseDirectory;
    public Dictionary<string, string> Scenes { get; private set; }
  }
}

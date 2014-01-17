using System;
using System.Collections.Generic;
using System.IO;

namespace aPC.Client
{
  class ArgumentReader
  {
    public ArgumentReader(List<string> xiArguments)
    {
      if (xiArguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      if (xiArguments[0] == @"/F")
      {
        // File passed in
        SceneXml = RetrieveFile(xiArguments[1]);
      }
      else if (xiArguments[0] == @"/I")
      {
        // (Integrated) Scene passed in
        SceneName = xiArguments[1];
      }
      else
      {
        throw new UsageException("Unexpected first argument");
      }
    }

    private string RetrieveFile(string xifilePath)
    {
      string lInputFilePath;

      try
      {
        lInputFilePath = Path.GetFullPath(xifilePath);
      }
      catch
      {
        // File not there / error
        throw new UsageException("Input was not a valid path (a full path is required)");
      }

      using (var lReader = new StreamReader(lInputFilePath))
      {
        return lReader.ReadToEnd();
      }
    }


    // The scene is integrated if a Scene name is specified
    // It isn't integrated if an Xml scene is given
    public bool IsIntegratedScene
    {
      get
      {
        if (!string.IsNullOrEmpty(SceneName))
        {
          return true;
        }
        else if (!string.IsNullOrEmpty(SceneXml))
        {
          return false;
        }
        throw new InvalidOperationException("Attempted to access scene information before any data is available");
      }
    }

    public string SceneName { get; private set; }
    public string SceneXml { get; private set; }
  }
}

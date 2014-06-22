using System.Collections.Generic;
using System.IO;

namespace aPC.Client
{
  public class ArgumentReader
  {
    public ArgumentReader(List<string> xiArguments)
    {
      mArgs = xiArguments;
    }

    public Settings ParseArguments()
    {
      if (mArgs.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      var lSettings = new Settings();

      switch (mArgs[0].ToLower())
      {
        case @"/i":
          lSettings.IsIntegratedScene = true;
          lSettings.SceneData = mArgs[1];
          break;
        case @"/f":
          lSettings.IsIntegratedScene = false;
          lSettings.SceneData = RetrieveFile(mArgs[1]);
          break;
        default:
          throw new UsageException("Unexpected first argument");
      }

      return lSettings;
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

    private readonly List<string> mArgs;
  }
}

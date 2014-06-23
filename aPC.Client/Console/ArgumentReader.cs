using System.Collections.Generic;
using System.IO;

namespace aPC.Client.Console
{
  public class ArgumentReader
  {
    public ArgumentReader(List<string> xiArguments)
    {
      mLocalSettings = Settings.NewInstance;
      ReadArguments(xiArguments);
    }

    private void ReadArguments(List<string> xiArguments)
    {
      if (xiArguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      switch (xiArguments[0].ToLower())
      {
        case @"/i":
          mLocalSettings.IsIntegratedScene = true;
          mLocalSettings.SceneData = xiArguments[1];
          break;
        case @"/f":
          mLocalSettings.IsIntegratedScene = false;
          mLocalSettings.SceneData = RetrieveFile(xiArguments[1]);
          break;
        default:
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
        throw new UsageException("Input was not a valid path");
      }

      using (var lReader = new StreamReader(lInputFilePath))
      {
        return lReader.ReadToEnd();
      }
    }

    public void AddArgumentsToSettings()
    {
      Settings.Instance.IsIntegratedScene = mLocalSettings.IsIntegratedScene;
      Settings.Instance.SceneData = mLocalSettings.SceneData;
    }

    private Settings mLocalSettings;
  }
}

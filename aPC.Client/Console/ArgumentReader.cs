using System.Collections.Generic;
using System.IO;

namespace aPC.Client.Console
{
  public class ArgumentReader
  {
    public ArgumentReader(List<string> xiArguments)
    {
      mLocalSettings = new Settings();
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
          mLocalSettings.Update(true, xiArguments[1]);
          break;
        case @"/f":
          mLocalSettings.Update(false, RetrieveFile(xiArguments[1]));
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

    public void AddArgumentsToSettings(Settings xiSettings)
    {
      xiSettings.Update(mLocalSettings.IsIntegratedScene, mLocalSettings.SceneData);
    }

    private readonly Settings mLocalSettings;
  }
}
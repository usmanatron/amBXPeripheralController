using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aPC.Client.Console
{
  public class ArgumentReader
  {
    public ArgumentReader(IEnumerable<string> xiArguments)
    {
      mLocalSettings = new Settings();
      ReadArguments(xiArguments.ToList());
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
      try
      {
        var lInputFilePath = Path.GetFullPath(xifilePath);

        using (var lReader = new StreamReader(lInputFilePath))
        {
          return lReader.ReadToEnd();
        }
      }
      catch
      {
        // File not there / error
        throw new UsageException("Input was not a valid path");
      }
    }

    public void AddArgumentsToSettings(Settings xiSettings)
    {
      xiSettings.Update(mLocalSettings.IsIntegratedScene, mLocalSettings.SceneData);
    }

    private readonly Settings mLocalSettings;
  }
}
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aPC.Client.Console
{
  public class ArgumentReader
  {
    private readonly Settings localSettings;

    public ArgumentReader(IEnumerable<string> arguments)
    {
      localSettings = new Settings();
      ReadArguments(arguments.ToList());
    }

    private void ReadArguments(List<string> arguments)
    {
      if (arguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      switch (arguments[0].ToLower())
      {
        case @"/i":
          localSettings.Update(true, arguments[1]);
          break;
        case @"/f":
          localSettings.Update(false, RetrieveFile(arguments[1]));
          break;
        default:
          throw new UsageException("Unexpected first argument");
      }
    }

    private string RetrieveFile(string filePath)
    {
      try
      {
        var inputFilePath = Path.GetFullPath(filePath);

        using (var reader = new StreamReader(inputFilePath))
        {
          return reader.ReadToEnd();
        }
      }
      catch
      {
        // File not there / error
        throw new UsageException("Input was not a valid path");
      }
    }

    public void AddArgumentsToSettings(Settings settings)
    {
      settings.Update(localSettings.IsIntegratedScene, localSettings.SceneData);
    }
  }
}
using aPC.Client.Shared;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aPC.Client.Cli
{
  public class ArgumentReader
  {
    private Settings settings;
    private List<string> arguments;
    
    public ArgumentReader(Settings settings, IEnumerable<string> arguments)
    {
      this.settings = settings;
      this.arguments = arguments.ToList();
    }

    public Settings Read()
    {
      if (arguments.Count != 2)
      {
        throw new UsageException("Unexpected number of arguments");
      }

      switch (arguments[0].ToLower())
      {
        case @"/i":
          settings.SetSceneName(arguments[1]);
          break;
        case @"/f":
          //TODO Fix
          //settings.SetScene(RetrieveFile(arguments[1]));
          break;
        default:
          throw new UsageException("Unexpected first argument");
      }

      return settings;
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
  }
}
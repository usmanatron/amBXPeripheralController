using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aPC.Client.Console
{
  public class ArgumentReader
  {
    private List<string> arguments;
    
    public ArgumentReader(IEnumerable<string> arguments)
    {
      this.arguments = arguments.ToList();
    }

    public void ReadInto(Settings settings)
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
          //settings.SetScene(RetrieveFile(arguments[1]));
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
  }
}
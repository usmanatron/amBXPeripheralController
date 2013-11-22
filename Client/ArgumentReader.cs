using System.Collections.Generic;
using System.IO;
using Common.Entities;

namespace Client
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
        IsIntegratedScene = false;
        CustomScene = RetrieveAndParseFile(xiArguments[1]);
        // File passed in
      }
      else if (xiArguments[0] == @"/I")
      {
        // (Integrated) Scene passed in
        IsIntegratedScene = true;
        SceneName = xiArguments[1];
      }
      else
      {
        throw new UsageException("Unexpected first argument");
      }
    }

    private amBXScene RetrieveAndParseFile(string xifilePath)
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

      return amBXSceneSerialiser.Deserialise(lInputFilePath);
    }

    public bool IsIntegratedScene { get; private set; }

    // When IsIntegrated Scene is true:
    // * SceneName is specified
    // * CustomScene is null
    // this is vice versa when IsIntegratedScene is false
    public string SceneName { get; private set; }
    public amBXScene CustomScene { get; private set; }
  }
}

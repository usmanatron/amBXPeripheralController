using System;
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

      Settings lSettings;

      switch (mArgs[0].ToLower())
      {
        case @"/i":
          lSettings = new Settings(false, mArgs[1]);
          break;
        case @"/f":
          lSettings = new Settings(false, RetrieveFile(mArgs[1]));
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

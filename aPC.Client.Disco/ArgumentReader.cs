using aPC.Common.Client;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Disco
{
  public class ArgumentReader
  {
    public ArgumentReader(List<string> xiArgs, Settings xiSettings)
    {
      mArgs = xiArgs;
      mSettings = xiSettings;
    }

    public Settings ParseArguments()
    {
      foreach (string lArg in mArgs)
      {
        var lDeconstructedArgument = lArg.Split(':');

        switch (lDeconstructedArgument[0].ToLower())
        {
          case "bpm":
            mSettings.BPM = int.Parse(lDeconstructedArgument[1]);
            break;
          case "intensity":
            mSettings.LightIntensityWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "red":
            mSettings.RedColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "blue":
            mSettings.BlueColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "green":
            mSettings.GreenColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "servers":
            mSettings.HostnameAccessor.ResetWith(lDeconstructedArgument[1].Split(',').ToList());
            break;
          default:
            throw new UsageException("Unknown argument: " + lDeconstructedArgument);
        }
      }

      return mSettings;
    }

    private Range GetRange(string xiRange)
    {
      var lDeconstructedWidth = xiRange.Split(',');
      if (lDeconstructedWidth.Count() != 2)
      {
        throw new UsageException("Invalid number of arguments when calculating a range: " + xiRange);
      }

      var lMinimum = float.Parse(lDeconstructedWidth[0]);
      var lMaximum = float.Parse(lDeconstructedWidth[1]);

      if (lMinimum < 0 || lMaximum > 1)
      {
        var lMessage = string.Format("Invalid input range {0} - must be between 0 and 1", xiRange);
        throw new UsageException(lMessage);
      }

      return new Range(lMinimum, lMaximum);
    }

    private List<string> mArgs;
    private Settings mSettings;    
  }
}

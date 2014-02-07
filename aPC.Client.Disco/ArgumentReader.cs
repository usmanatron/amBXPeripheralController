using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Disco
{
  public class ArgumentReader
  {
    public ArgumentReader(List<string> xiArgs)
    {
      mArgs = xiArgs;
    }

    public Settings ParseArguments()
    {
      var lSettings = new Settings();

      foreach (string lArg in mArgs)
      {
        var lDeconstructedArgument = lArg.Split(':');

        switch (lDeconstructedArgument[0].ToLower())
        {
          case "bpm":
            lSettings.BPM = int.Parse(lDeconstructedArgument[1]);
            break;
          case "lightintensity":
            lSettings.LightIntensityWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "red":
            lSettings.RedColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "blue":
            lSettings.BlueColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "green":
            lSettings.GreenColourWidth = GetRange(lDeconstructedArgument[1]);
            break;
          case "fan":
            lSettings.FanWidth = GetRange(lDeconstructedArgument[1]);
            break;
          default:
            throw new UsageException("Unknown argument: " + lDeconstructedArgument);
        }
      }

      return lSettings;
    }

    private Range GetRange(string xiRange)
    {
      var lDeconstructedWidth = xiRange.Split(',');
      if (lDeconstructedWidth.Count() != 2)
      {
        throw new UsageException("Invalid number of arguments when calculating a range: " + xiRange);
      }

      return new Range(float.Parse(lDeconstructedWidth[0]),
                       float.Parse(lDeconstructedWidth[1]));
    }

    private List<string> mArgs;
  }
}

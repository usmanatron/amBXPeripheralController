using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Disco
{
  public class ArgumentReader
  {
    private List<string> args;
    private Settings settings;

    public ArgumentReader(List<string> args, Settings sSettings)
    {
      this.args = args;
      this.settings = sSettings;
    }

    public Settings ParseArguments()
    {
      foreach (string arg in args)
      {
        var deconstructedArgument = arg.Split(':');

        switch (deconstructedArgument[0].ToLower())
        {
          case "bpm":
            settings.BPM = int.Parse(deconstructedArgument[1]);
            break;
          case "intensity":
            settings.LightIntensityWidth = GetRange(deconstructedArgument[1]);
            break;
          case "red":
            settings.RedColourWidth = GetRange(deconstructedArgument[1]);
            break;
          case "blue":
            settings.BlueColourWidth = GetRange(deconstructedArgument[1]);
            break;
          case "green":
            settings.GreenColourWidth = GetRange(deconstructedArgument[1]);
            break;
          case "servers":
            settings.HostnameAccessor.ResetWith(deconstructedArgument[1].Split(',').ToList());
            break;
          default:
            throw new UsageException("Unknown argument: " + deconstructedArgument);
        }
      }

      return settings;
    }

    private Range GetRange(string range)
    {
      var deconstructedWidth = range.Split(',');
      if (deconstructedWidth.Count() != 2)
      {
        throw new UsageException("Invalid number of arguments when calculating a range: " + range);
      }

      var minimum = float.Parse(deconstructedWidth[0]);
      var maximum = float.Parse(deconstructedWidth[1]);

      if (minimum < 0 || maximum > 1)
      {
        var message = string.Format("Invalid input range {0} - must be between 0 and 1", range);
        throw new UsageException(message);
      }

      return new Range(minimum, maximum);
    }
  }
}
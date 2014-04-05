using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Morse
{
  class Morse
  {
    static void Main(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs);
      var lGenerator = new SceneGenerator(lSettings).Generate();
      //PushScene(lGenerator);
    }

    private static Settings GetSettings(string[] xiArgs)
    {
      try
      {
        var lArguments = string.Join(" ", xiArgs);
        return new ArgumentReader(lArguments).Read();
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        throw;
      }
    }
  }
}

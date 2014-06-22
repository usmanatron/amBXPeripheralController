using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client
{
  public class Client
  {
    public static void ConsoleMain(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs.ToList());
      ConsoleMain(lSettings);
    }

    public static void ConsoleMain(Settings mSettings)
    {
      var lKernel = new NinjectKernelHandler(mSettings);
      var lTask = lKernel.Get<ClientTask>();
      lTask.Push();
    }

    private static Settings GetSettings(IEnumerable<string> xiArgs)
    {
      try
      {
        return new ArgumentReader(xiArgs.ToList()).ParseArguments();
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        throw;
      }
    }
  }
}

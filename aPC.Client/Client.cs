using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client
{
  public class Client
  {
    public static void Main(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs.ToList());
      var lKernel = new NinjectKernelHandler(lSettings);
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
        Environment.Exit(1);
        throw;
      }
    }
  }
}

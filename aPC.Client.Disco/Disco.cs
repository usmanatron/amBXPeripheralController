using System;
using System.Linq;
using System.Collections.Generic;
using aPC.Common.Client;

namespace aPC.Client.Disco
{
  class Disco
  {
    public static void Main(string[] xiArgs)
    {
      var lKernel = new NinjectKernelHandler();
      BuildSettings(xiArgs.ToList(), lKernel.Get<Settings>());
      
      var lTask = lKernel.Get<DiscoTask>();
      lTask.Run();
    }

    private static void BuildSettings(IEnumerable<string> xiArgs, Settings xiSettings)
    {
      try
      {
        new ArgumentReader(xiArgs.ToList(), xiSettings).ParseArguments();
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

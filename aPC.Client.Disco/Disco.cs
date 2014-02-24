using System;
using System.Linq;
using System.Collections.Generic;

namespace aPC.Client.Disco
{
  class Disco
  {
    public static void Main(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs.ToList());
      var lKernel = new NinjectKernelHandler(lSettings);
      
      var lTask = lKernel.Get<DiscoTask>();
      lTask.Run();
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

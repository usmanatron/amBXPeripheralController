using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Disco
{
  internal class Disco
  {
    public static void Main(string[] args)
    {
      var kernel = new NinjectKernelHandler();
      BuildSettings(args.ToList(), kernel.Get<Settings>());

      var task = kernel.Get<DiscoTask>();
      task.Run();
    }

    private static void BuildSettings(IEnumerable<string> args, Settings settings)
    {
      try
      {
        new ArgumentReader(args.ToList(), settings).ParseArguments();
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
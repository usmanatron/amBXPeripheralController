using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client
{
  internal class Client
  {
    private static void Main(string[] xiArgs)
    {
      var lSettings = GetSettings(xiArgs.ToList());
      var lClientTask = new ClientTask(lSettings);
      lClientTask.Push();
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

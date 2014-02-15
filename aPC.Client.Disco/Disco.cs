using System;
using System.Linq;

namespace aPC.Client.Disco
{
  class Disco
  {
    public static void Main(string[] xiArgs)
    {
      Settings lSettings;
      try
      {
        lSettings = new ArgumentReader(xiArgs.ToList()).ParseArguments();
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        Environment.Exit(1);
        return;
      }
      
      new DiscoTask(lSettings).Run();
    }
  }
}

﻿using System.Linq;

namespace aPC.Client.Disco
{
  class Disco
  {
    public static void Main(string[] xiArgs)
    {
      var lSettings = new ArgumentReader(xiArgs.ToList()).ParseArguments();
      new DiscoTask(lSettings).Run();
    }
  }
}

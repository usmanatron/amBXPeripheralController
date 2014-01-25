using System.Linq;
using System.Collections.Generic;

namespace aPC.Client.Disco
{
  class Disco
  {
    static void Main(string[] xiArgs)
    {
      var lSettings = new ArgumentReader(xiArgs.ToList()).ParseArguments();
      new DiscoTask(lSettings).Run();
    }
  }
}

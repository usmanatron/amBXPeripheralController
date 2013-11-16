using System;

namespace Server
{
  internal class Server
  {
    private static void Main(string[] args)
    {
      try
      {
        var lServer = new ServerTask();
        lServer.Run();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        Console.WriteLine(e.StackTrace);
        Environment.Exit(1);
      }

      
    }
  }
}

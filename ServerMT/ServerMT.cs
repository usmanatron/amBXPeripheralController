namespace ServerMT
{
  class ServerMT
  {
    private static void Main(string[] args)
    {
      var lServer = new ServerTask();
      lServer.Run();
    }
  }
}

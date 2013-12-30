namespace ServerMT
{
  class ServerMT
  {
    private static void Main(string[] args)
    {
      ServerTask = new ServerTask();
      ServerTask.Run();
    }

    internal static ServerTask ServerTask;
  }
}

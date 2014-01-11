namespace Server
{
  class Server
  {
    private static void Main(string[] args)
    {
      ServerTask = new ServerTask();
      ServerTask.Run();
    }

    internal static ServerTask ServerTask;
  }
}

namespace Client
{
  internal class Client
  {
    private static void Main(string[] args)
    {
      var lClientTask = new ClientTask(args);
      lClientTask.Push();
    }
  }
}

using aPC.Client.Morse.Communication;

namespace aPC.Client.Morse
{
  internal class Morse
  {
    private static void Main(string[] args)
    {
      try
      {
        var arguments = string.Join(" ", args);
        var settings = new ArgumentReader(arguments).Read();
        var generatedScene = new SceneGenerator(settings).Generate();

        new NotificationClient().PushCustomScene(generatedScene);
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
      }
    }
  }
}
using aPC.Client.Morse.Communication;

namespace aPC.Client.Morse
{
  internal class Morse
  {
    private static void Main(string[] args)
    {
      var settings = GetSettings(args);
      var generatedScene = new SceneGenerator(settings).Generate();
      new NotificationClient().PushCustomScene(generatedScene);
    }

    private static Settings GetSettings(string[] args)
    {
      try
      {
        var arguments = string.Join(" ", args);
        return new ArgumentReader(arguments).Read();
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
        throw;
      }
    }
  }
}
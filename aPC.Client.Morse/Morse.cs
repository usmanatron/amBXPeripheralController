using aPC.Client.Morse.Communication;
using aPC.Client.Morse.Translators;
using aPC.Common.Client;

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
        var translator = new MessageTranslator(new WordTranslator(new CharacterTranslator()));
        var generatedScene = new SceneGenerator(translator).Generate(settings);

        new NotificationClient(new HostnameAccessor()).PushCustomScene(generatedScene);
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
      }
    }
  }
}
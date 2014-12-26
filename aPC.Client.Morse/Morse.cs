using aPC.Client.Morse.Communication;
using aPC.Client.Morse.Translators;
using aPC.Common.Builders;
using aPC.Common.Client;
using Ninject;

namespace aPC.Client.Morse
{
  internal class Morse
  {
    private static void Main(string[] args)
    {
      try
      {
        var kernel = new NinjectKernelHandler(string.Join(" ", args)).Kernel;
        var settings = kernel.Get<ArgumentReader>().Read();
        var generatedScene = kernel.Get<SceneGenerator>().Generate(settings);

        kernel.Get<NotificationClient>().PushCustomScene(generatedScene);
      }
      catch (UsageException e)
      {
        e.DisplayUsage();
      }
    }
  }
}
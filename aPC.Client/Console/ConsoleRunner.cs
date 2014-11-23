using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace aPC.Client.Console
{
  internal class ConsoleRunner
  {
    [DllImport("Kernel32.dll")]
    private static extern bool AllocConsole();

    private readonly SceneRunner sceneRunner;

    public ConsoleRunner(Settings settings, SceneRunner sceneRunner, List<string> arguments)
    {
      this.sceneRunner = sceneRunner;
      var reader = new ArgumentReader(arguments);
      reader.AddArgumentsToSettings(settings);
    }

    public void Run()
    {
      AllocateConsole();

      try
      {
        sceneRunner.RunScene();
      }
      catch (UsageException exception)
      {
        exception.DisplayUsage();
        System.Console.ReadLine();
      }
    }

    private void AllocateConsole()
    {
      AllocConsole();
    }
  }
}
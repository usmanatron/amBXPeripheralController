using System.Collections.Generic;

namespace aPC.Client.Console
{
  internal class ConsoleRunner
  {
    private readonly SceneRunner sceneRunner;

    public ConsoleRunner(Settings settings, SceneRunner sceneRunner, IEnumerable<string> arguments)
    {
      this.sceneRunner = sceneRunner;
      var reader = new ArgumentReader(arguments);
      reader.ReadInto(settings);
    }

    public void Run()
    {
      NativeMethods.AllocConsole();

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
  }
}
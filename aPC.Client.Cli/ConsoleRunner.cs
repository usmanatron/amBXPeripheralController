using aPC.Client.Shared;
using System.Collections.Generic;

namespace aPC.Client.Cli
{
  internal class ConsoleRunner
  {
    private readonly SceneRunner sceneRunner;
    private Settings settings;

    public ConsoleRunner(SceneRunner sceneRunner, ArgumentReader argumentReader)
    {
      this.sceneRunner = sceneRunner;
      this.settings = argumentReader.Read();
    }

    public void Run()
    {
      try
      {
        sceneRunner.RunScene(settings);
      }
      catch (UsageException exception)
      {
        exception.DisplayUsage();
        System.Console.ReadLine();
      }
    }
  }
}
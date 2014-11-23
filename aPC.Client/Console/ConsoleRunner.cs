﻿using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace aPC.Client.Console
{
  internal class ConsoleRunner
  {
    public ConsoleRunner(Settings xiSettings, SceneRunner xiSceneRunner, List<string> xiArguments)
    {
      mSceneRunner = xiSceneRunner;
      var lReader = new ArgumentReader(xiArguments);
      lReader.AddArgumentsToSettings(xiSettings);
    }

    public void Run()
    {
      AllocateConsole();

      try
      {
        mSceneRunner.RunScene();
      }
      catch (UsageException lException)
      {
        lException.DisplayUsage();
        System.Console.ReadLine();
      }
    }

    private void AllocateConsole()
    {
      AllocConsole();
    }

    [DllImport("Kernel32.dll")]
    private static extern bool AllocConsole();

    private readonly SceneRunner mSceneRunner;
  }
}
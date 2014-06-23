using aPC.Common;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Ninject;

namespace aPC.Client.Console
{
  class ConsoleRunner
  {
    public ConsoleRunner(List<string> xiArguments)
    {
      var lReader = new ArgumentReader(xiArguments);
      lReader.AddArgumentsToSettings();
    }

    public void Run()
    {
      AllocateConsole();

      try
      {
        var lTask = NinjectKernelHandler.Instance.Kernel.Get<SceneRunner>();
        lTask.RunScene();
      }
      catch (UsageException lException)
      {
        lException.DisplayUsage();
        System.Console.ReadLine();
      }
    }

    private void AllocateConsole()
    {
      var parentId = ParentProcessUtilities.GetParentProcess(Process.GetCurrentProcess().Id).Id;
      AllocConsole();
    }

    [DllImport("Kernel32.dll")]
    private static extern bool AllocConsole();
  }
}

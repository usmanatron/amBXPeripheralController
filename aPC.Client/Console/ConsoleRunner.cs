using aPC.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Client;
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
        var lTask = NinjectKernelHandler.Instance.Kernel.Get<ClientTask>();
        lTask.Push();
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

    List<string> mArguments;

    [DllImport("Kernel32.dll")]
    private static extern bool AllocConsole();
  }
}

using aPC.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Console
{
  class ConsoleRunner
  {
    public ConsoleRunner(List<string> xiArguments)
    {
      mArguments = xiArguments;
    }

    public void Run()
    {
      AllocateConsole();

      try
      {
        //qqUMI This is duplicated in the MainWindow class - commonise?
        // Also need to sort out DI properly...
        var lSettings = new ArgumentReader(mArguments).ParseArguments();
        var lKernel = new NinjectKernelHandler(lSettings);
        var lTask = lKernel.Get<ClientTask>();
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

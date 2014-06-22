using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using aPC.Common;
using System.Threading;

namespace aPC.Client
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    /// <summary>
    ///   Checks if arguments have been passed in - if this is the case,
    ///   fallback to running in Console mode (i.e. suppress the UI).
    /// </summary>
    protected override void OnStartup(StartupEventArgs e)
    {
      var lArguments = GetArguments();

      if (lArguments.Count > 0)
      {
        RunInConsole(lArguments);
      }
    }

    /// <remarks>
    /// When retrieving command line arguments in this way, the first
    /// argument is always the name of the executable.
    /// </remarks>
    private List<string> GetArguments()
    {
      var lArgs = Environment.GetCommandLineArgs();

      return lArgs
        .Skip(1)
        .Take(lArgs.Count() - 1)
        .ToList();
    }

    private void RunInConsole(List<string> xiArguments)
    {
      AllocateConsole();
      
      try
      {
        //qqUMI This is duplicated in the MainWindow class - commonise?
        // Also need to sort out DI properly...
        var lSettings = new ArgumentReader(xiArguments).ParseArguments();
        var lKernel = new NinjectKernelHandler(lSettings);
        var lTask = lKernel.Get<ClientTask>();
        lTask.Push();
      }
      catch (UsageException lException)
      {
        lException.DisplayUsage();
        Console.ReadLine();
      }
      Shutdown(0);
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

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

namespace aPC.Client.Overlay
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

    private void RunInConsole(List<string> xiArguments)
    {
      var parentId = ParentProcessUtilities.GetParentProcess(Process.GetCurrentProcess().Id).Id;

      AllocConsole();

      try
      {
        Client.Main(xiArguments.ToArray());
      }
      catch
      {
        Console.ReadLine();
      }
      Shutdown(0);
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

    [DllImport("Kernel32.dll")]
    private static extern bool AllocConsole();
  }
}

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
    protected override void OnStartup(StartupEventArgs e)
    {
      var lArguments = GetArguments();

      if (lArguments.Count > 0)
      {
        var parentId = ParentProcessUtilities.GetParentProcess(Process.GetCurrentProcess().Id).Id;
        AttachConsole(parentId);
        
        Console.WriteLine("test");
        Client.Main(lArguments.ToArray());
        Thread.Sleep(1000);
        Shutdown(0);
      }

      int i = 1;
    }

    // When retrieving command line arguments in this way, the first
    // argument is always the name of the executable.
    private List<string> GetArguments()
    {
      var lArgs = Environment.GetCommandLineArgs();
      
      return lArgs
        .Skip(1)
        .Take(lArgs.Count() - 1)
        .ToList();
    }

    [DllImport("Kernel32.dll")]
    public static extern bool AttachConsole(int processId);
  }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using aPC.Common;
using aPC.Client.Console;
using aPC.Common.Client;

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
        var lRunner = new ConsoleRunner(lArguments);
        lRunner.Run();
        Shutdown(0);
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
  }
}

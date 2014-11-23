using aPC.Client.Communication;
using aPC.Client.Console;
using aPC.Client.Scene;
using aPC.Common.Communication;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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
      AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
      var lKernel = SetupKernel();
      var lArguments = GetArguments();

      if (lArguments.Count > 0)
      {
        lKernel.Bind<ConsoleRunner>().ToSelf().WithConstructorArgument("xiArguments", lArguments);
        var lRunner = lKernel.Get<ConsoleRunner>();
        lRunner.Run();
      }
      else
      {
        var lMainWindow = lKernel.Get<MainWindow>();
        lMainWindow.ShowDialog();
      }
      Shutdown(0);
    }

    private StandardKernel SetupKernel()
    {
      var lKernel = new StandardKernel();

      lKernel.Bind<Settings>().ToSelf().InSingletonScope();
      lKernel.Bind<INotificationClient>().To<NotificationClient>();
      lKernel.Bind<IntegratedListing>().ToSelf().InSingletonScope();
      lKernel.Bind<CustomListing>().ToSelf().InSingletonScope();

      return lKernel;
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

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var lFilePath = Path.Combine(Environment.CurrentDirectory, "Exception.log");
      System.IO.File.WriteAllText(lFilePath, e.ExceptionObject.ToString());
    }
  }
}
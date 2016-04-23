using aPC.Client.Gui.Scene;
using aPC.Client.Shared;
using aPC.Common.Communication;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace aPC.Client.Gui
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
      var kernel = SetupKernel();
      var arguments = GetArguments();

      var mainWindow = kernel.Get<MainWindow>();
      mainWindow.ShowDialog();
      
      Shutdown(0);
    }

    private StandardKernel SetupKernel()
    {
      var kernel = new StandardKernel();

      kernel.Bind<Settings>().ToSelf().InSingletonScope();
      kernel.Bind<INotificationClient>().To<NotificationClient>();
      kernel.Bind<IntegratedListing>().ToSelf().InSingletonScope();
      kernel.Bind<CustomListing>().ToSelf().InSingletonScope();

      return kernel;
    }

    /// <remarks>
    /// When retrieving command line arguments in this way, the first
    /// argument is always the name of the executable.
    /// </remarks>
    private List<string> GetArguments()
    {
      var args = Environment.GetCommandLineArgs();

      return args
        .Skip(1)
        .Take(args.Count() - 1)
        .ToList();
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      var filePath = Path.Combine(Environment.CurrentDirectory, "Exception.log");
      File.WriteAllText(filePath, e.ExceptionObject.ToString());
    }
  }
}
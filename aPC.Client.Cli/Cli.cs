using System;
using System.Collections.Generic;
using Ninject;
using aPC.Common.Communication;
using aPC.Client.Shared;

namespace aPC.Client.Cli
{
  class Cli
  {
    static void Main(string[] args)
    {
      var kernel = SetupKernel(args);
      var runner = kernel.Get<ConsoleRunner>();
      runner.Run();
    }

    private static StandardKernel SetupKernel(IEnumerable<string> args)
    {
      var kernel = new StandardKernel();

      kernel.Bind<Settings>().ToSelf().InSingletonScope();
      kernel.Bind<ArgumentReader>().ToSelf().WithConstructorArgument("arguments", args);
      kernel.Bind<INotificationClient>().To<NotificationClient>();
      kernel.Bind<ConsoleRunner>().ToSelf();

      return kernel;
    }
  }
}

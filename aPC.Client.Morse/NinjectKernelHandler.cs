using aPC.Common.Client;
using Ninject;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  internal class NinjectKernelHandler
  {
    public StandardKernel Kernel;

    public NinjectKernelHandler(string arguments)
    {
      Kernel = new StandardKernel();

      Kernel.Bind<ArgumentReader>().ToSelf()
        .InSingletonScope()
        .WithConstructorArgument("arguments", arguments);
    }
  }
}
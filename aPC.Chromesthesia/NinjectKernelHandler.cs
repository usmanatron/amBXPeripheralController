using aPC.Chromesthesia.Sound;
using aPC.Common.Client;
using NAudio.Wave;
using Ninject;

namespace aPC.Chromesthesia
{
  internal class NinjectKernelHandler
  {
    public StandardKernel Kernel { get; private set; }

    public NinjectKernelHandler()
    {
      Kernel = new StandardKernel();
      Kernel.Bind<IWaveIn>().To<WasapiLoopbackCapture>();
      Kernel.Bind<IPitchDetector>().To<FftPitchDetector>();
    }
  }
}
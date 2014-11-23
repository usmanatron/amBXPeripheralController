using aPC.Chromesthesia.Pitch;
using aPC.Common.Client;
using NAudio.Wave;

namespace aPC.Chromesthesia
{
  internal class NinjectKernelHandler : NinjectKernelHandlerBase
  {
    protected override void SetupBindings()
    {
      kernel.Bind<IWaveIn>().To<WasapiLoopbackCapture>();
      kernel.Bind<IPitchDetector>().To<FftPitchDetector>();
    }
  }
}
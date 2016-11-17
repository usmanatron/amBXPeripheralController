using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  public class PreRunFrame : IPreRunComponent
  {
    public amBXScene Scene { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public PreRunFrame(amBXScene scene, AtypicalFirstRunInfiniteTicker ticker)
    {
      Scene = scene;
      Ticker = ticker;
    }
  }
}

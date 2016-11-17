using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  public class DirectionalPreRunComponent : IPreRunComponent
  {
    public amBXScene Scene { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public AtypicalFirstRunInfiniteTicker Ticker { get; private set; }

    public DirectionalPreRunComponent(amBXScene scene, DirectionalComponent directionalComponent, AtypicalFirstRunInfiniteTicker ticker)
    {
      Scene = scene;
      DirectionalComponent = directionalComponent;
      Ticker = ticker;
    }
  }
}

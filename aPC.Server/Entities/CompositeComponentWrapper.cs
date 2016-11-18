using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  public class CompositeComponentWrapper : ComponentWrapperBase
  {
    public DirectionalComponent DirectionalComponent { get; private set; }

    public CompositeComponentWrapper(amBXScene scene, DirectionalComponent directionalComponent, AtypicalFirstRunInfiniteTicker ticker) 
      : base(scene, ticker)
    {
      DirectionalComponent = directionalComponent;
    }

    public override eSceneType ComponentType => eSceneType.Composite;
  }
}

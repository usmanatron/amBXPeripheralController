using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  public class SingularComponentWrapper : ComponentWrapperBase
  {
    public SingularComponentWrapper(amBXScene scene, AtypicalFirstRunInfiniteTicker ticker) 
      : base(scene, ticker)
    {
    }

    public override eSceneType ComponentType => eSceneType.Composite;
  }
}

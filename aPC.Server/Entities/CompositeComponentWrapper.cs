using System.Collections.Generic;
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

    public override IEnumerable<DirectionalComponent> GetNextComponentsToRun()
    {
      var frame = GetFrame();

      var directionalComponentFromWrapper = DirectionalComponent;
      yield return frame
        .GetComponentInDirection(directionalComponentFromWrapper.ComponentType, directionalComponentFromWrapper.Direction);
    }
  }
}

using System;
using aPC.Common.Entities;
using aPC.Server.Snapshots;

namespace aPC.Server.SceneHandlers
{
  public abstract class ComponentHandler<T> : SceneHandlerBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentHandler(amBXScene xiScene, Action xiEventComplete)
      : base(xiScene, xiEventComplete)
    {
    }
  }
}

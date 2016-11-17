using aPC.Common.Entities;
using System.Threading;

namespace aPC.Server.Entities
{
  // A link to a running DirectionComponent.  Allows you to interact with it while still running
  public class DirectionalRunningComponent : IRunningComponent
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public DirectionalRunningComponent(CancellationTokenSource cancellationToken, DirectionalComponent directionalComponent)
    {
      CancellationToken = cancellationToken;
      DirectionalComponent = directionalComponent;
    }
  }
}
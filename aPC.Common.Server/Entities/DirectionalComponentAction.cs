using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.Entities
{
  public class DirectionalComponentAction
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public DirectionalComponentAction(CancellationTokenSource cancellationToken, DirectionalComponent directionalComponent)
    {
      CancellationToken = cancellationToken;
      DirectionalComponent = directionalComponent;
    }
  }
}
using aPC.Common;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.ServerV3.Entities
{
  public class DirectionalComponentAction
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public DirectionalComponentAction(CancellationTokenSource cancellationToken, DirectionalComponent directionalComponent)
    {
      this.CancellationToken = cancellationToken;
      this.DirectionalComponent = directionalComponent;
    }
  }
}
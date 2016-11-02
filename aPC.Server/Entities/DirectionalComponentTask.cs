using aPC.Common.Entities;
using System.Threading;

namespace aPC.Server.Entities
{
  public class DirectionalComponentTask
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public DirectionalComponent DirectionalComponent { get; private set; }

    public DirectionalComponentTask(CancellationTokenSource cancellationToken, DirectionalComponent directionalComponent)
    {
      CancellationToken = cancellationToken;
      DirectionalComponent = directionalComponent;
    }
  }
}
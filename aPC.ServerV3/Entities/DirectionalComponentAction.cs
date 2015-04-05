using aPC.Common;
using System.Threading;
using System.Threading.Tasks;

namespace aPC.ServerV3.Entities
{
  internal class DirectionalComponentAction
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public eComponentType? ComponentType { get; private set; }

    public eDirection Direction { get; private set; }

    public DirectionalComponentAction(CancellationTokenSource cancellationToken, eComponentType? compoenntType, eDirection direction)
    {
      this.CancellationToken = cancellationToken;
      this.ComponentType = compoenntType;
      this.Direction = direction;
    }
  }
}
using System.Threading;
using System.Threading.Tasks;

namespace aPC.ServerV3.Entities
{
  internal class CancellableTask
  {
    public Task Task { get; private set; }

    public CancellationTokenSource CancellationToken { get; private set; }

    public CancellableTask(Task task, CancellationTokenSource cancellationToken)
    {
      this.Task = task;
      this.CancellationToken = cancellationToken;
    }
  }
}
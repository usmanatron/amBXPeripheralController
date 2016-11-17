using System.Threading;

namespace aPC.Server.Entities
{
  public interface IRunningComponent
  {
    CancellationTokenSource CancellationToken { get; }
  }
}
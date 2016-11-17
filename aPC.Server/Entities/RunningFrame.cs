using System.Threading;

namespace aPC.Server.Entities
{
  // A link to a running DirectionComponent.  Allows you to interact with it while still running
  public class RunningFrame : IRunningComponent
  {
    public CancellationTokenSource CancellationToken { get; private set; }

    public RunningFrame(CancellationTokenSource cancellationToken)
    {
      CancellationToken = cancellationToken;
    }
  }
}
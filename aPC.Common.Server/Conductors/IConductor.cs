using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Conductors
{
  public interface IConductor
  {
    void Run();
    void RunOnce();

    void UpdateScene(amBXScene xiScene);

    void Enable();
    void Disable();

    LockedBool IsRunning { get; }
    eDirection Direction { get; }
    eComponentType ComponentType { get; }
  }
}

using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Snapshots;

namespace aPC.Server.Conductors
{
  public interface IConductor
  {
    void Run();
    void RunOnce();

    void UpdateScene(amBXScene xiScene);

    bool IsRunning { get; set; }
    void Enable();
    void Disable();

    eDirection Direction { get; }
    eComponentType ComponentType { get; }
  }
}

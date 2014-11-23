using aPC.Common.Entities;

namespace aPC.Common.Server.Conductors
{
  public interface IConductor
  {
    void Run();

    void RunOnce();

    void UpdateScene(amBXScene scene);

    void Enable();

    void Disable();

    Locked<bool> IsRunning { get; }

    eDirection Direction { get; }

    eComponentType ComponentType { get; }
  }
}
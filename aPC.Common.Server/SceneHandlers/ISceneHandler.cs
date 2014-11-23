using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.SceneHandlers
{
  internal interface ISceneHandler<T> where T : SnapshotBase
  {
    void UpdateScene(amBXScene xiNewScene);

    T GetNextSnapshot(eDirection xiDirection);

    void AdvanceScene();

    void Disable();

    void Enable();

    bool IsEnabled { get; set; }
  }
}
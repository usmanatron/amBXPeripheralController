using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.Snapshots;
namespace aPC.Server.SceneHandlers
{
  interface ISceneHandler<T> where T : SnapshotBase
  {
    void UpdateScene(amBXScene xiNewScene);

    T GetNextSnapshot(eDirection xiDirection);

    void AdvanceScene();

    void Disable();

    void Enable();

    bool IsEnabled { get; set; }
  }
}

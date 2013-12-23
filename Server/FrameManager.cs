using Common.Entities;
using Common.Server.Managers;

namespace Server
{
  public class FrameManager : ManagerBase<Frame>
  {
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      // All scenes are valid for this manager
      return true;
    }

    public override Data<Frame> GetNext()
    {
      var lFrame = base.GetNextFrame();
      return new Data<Frame>(lFrame, 0, lFrame.Length);
    }
  }
}

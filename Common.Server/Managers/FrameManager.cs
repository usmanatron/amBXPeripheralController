using Common.Entities;
using System.Linq;

namespace Common.Server.Managers
{
  public class FrameManager : ManagerBase<Frame>
  {
    public FrameManager() : base ()
    {
      SetupNewScene(mCurrentScene);
    }

    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      var lFrames = xiNewScene
        .Frames
        .Where(frame => frame.Lights != null || 
                        frame.Fans   != null || 
                        frame.Rumble != null);

      // All scenes are valid for this manager
      return lFrames.Any(frame => frame != null);
    }

    public override Data<Frame> GetNext()
    {
      var lFrame = base.GetNextFrame();
      return new Data<Frame>(lFrame, 0, lFrame.Length);
    }
  }
}

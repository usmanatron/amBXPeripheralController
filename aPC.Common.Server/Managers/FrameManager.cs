using aPC.Common.Entities;
using System.Linq;
using System;

namespace aPC.Common.Server.Managers
{
  public class FrameManager : ManagerBase
  {
    public FrameManager() 
      : this(null)
    {
    }

    public FrameManager(Action xiEventComplete)
      : base(xiEventComplete)
    {
      SetupNewScene(CurrentScene);
    }

    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      var lFrames = xiNewScene
        .Frames
        .Where(frame => frame.Lights != null || 
                        frame.Fans   != null || 
                        frame.Rumbles != null);

      // All scenes are valid for this manager
      return lFrames.Any(frame => frame != null);
    }

    public override Data GetNext()
    {
      var lFrame = GetNextFrame();
      return new FrameData(lFrame, 0, lFrame.Length);
    }
  }
}

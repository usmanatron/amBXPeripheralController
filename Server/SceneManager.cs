using Common.Entities;
using Common.Server.Managers;

namespace Server
{
  public class SceneManager : ManagerBase<Frame>
  {
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      // All scenes are valid for this manager
      return true;
    }

    public override Frame GetNext()
    {
      return base.GetNextFrame();
    }
  }
}

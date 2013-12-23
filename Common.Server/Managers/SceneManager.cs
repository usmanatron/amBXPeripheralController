using System.Linq;
using Common.Accessors;
using Common.Entities;

namespace Common.Server.Managers
{
  public class SceneManager : ManagerBase<Frame>
  {
    public SceneManager(amBXScene xiScene) : base(xiScene)
    {
    }

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

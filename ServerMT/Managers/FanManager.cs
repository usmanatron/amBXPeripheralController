using amBXLib;
using Common.Entities;
using Common.Integration;
using System.Linq;
using Common.Server.Managers;

namespace ServerMT.Managers
{
  class FanManager : ManagerBase<Fan>
  {
    public FanManager(CompassDirection xiDirection)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null fan in a "somewhat" correct direction defined.
    protected override bool SceneIsApplicable(amBXScene xiNewScene)
    {
      if (xiNewScene.IsSynchronised)
      {
        return false;
      }

      var lFans = xiNewScene
        .Frames
        .Select(frame => frame.Fans)
        .Where(component => component != null)
        .Select(component => CompassDirectionConverter.GetFan(mDirection, component));

      return lFans.Any(fan => fan != null);
    }

    public override Data<Fan> GetNext()
    {
      var lFrame = GetNextFrame();
      var lFan = CompassDirectionConverter.GetFan(mDirection, lFrame.Fans);

      return new Data<Fan>(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    readonly CompassDirection mDirection;
  }
}

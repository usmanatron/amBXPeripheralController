using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using amBXSceneV1 = aPC.SceneMigrator.EntitiesV1.amBXScene;
using FrameV1 = aPC.SceneMigrator.EntitiesV1.Frame;

namespace aPC.SceneMigrator
{
  internal class SceneMigrator
  {
    private LightSectionMigrator lightSectionMigrator;
    private FanSectionMigrator fanSectionMigrator;
    private RumbleSectionMigrator rumbleSectionMigrator;

    public SceneMigrator(LightSectionMigrator lightSectionMigrator, FanSectionMigrator fanSectionMigrator, RumbleSectionMigrator rumbleSectionMigrator)
    {
      this.lightSectionMigrator = lightSectionMigrator;
      this.fanSectionMigrator = fanSectionMigrator;
      this.rumbleSectionMigrator = rumbleSectionMigrator;
    }

    public amBXScene Migrate(amBXSceneV1 oldScene)
    {
      var newScene = new amBXScene();
      newScene.IsExclusive = oldScene.IsExclusive;
      newScene.SceneType = oldScene.SceneType;
      newScene.Frames = MigrateFrames(oldScene.Frames).ToList();

      return newScene;
    }

    private IEnumerable<Frame> MigrateFrames(IEnumerable<FrameV1> oldFrames)
    {
      foreach (var oldFrame in oldFrames)
      {
        yield return MigrateFrame(oldFrame);
      }
    }

    private Frame MigrateFrame(FrameV1 oldFrame)
    {
      return new Frame()
      {
        IsRepeated = oldFrame.IsRepeated,
        Length = oldFrame.Length,
        LightSection = lightSectionMigrator.Migrate(oldFrame.Lights),
        FanSection = fanSectionMigrator.Migrate(oldFrame.Fans),
        RumbleSection = rumbleSectionMigrator.Migrate(oldFrame.Rumbles)
      };
    }
  }
}
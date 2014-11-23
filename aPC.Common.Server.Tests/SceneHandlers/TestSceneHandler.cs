using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Tests.Snapshots;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  internal class TestSceneHandler : SceneHandlerBase<TestSnapshot>
  {
    public TestSceneHandler(amBXScene scene, Action action)
      : base(scene, action)
    {
    }

    public override TestSnapshot GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();
      return new TestSnapshot(frame.Length);
    }

    public Frame NextFrame
    {
      get
      {
        return GetNextFrame();
      }
    }

    public amBXScene Scene
    {
      get
      {
        return CurrentScene;
      }
    }

    public bool IsEnabled
    {
      get
      {
        return base.IsEnabled;
      }
      set
      {
        base.IsEnabled = value;
      }
    }
  }
}
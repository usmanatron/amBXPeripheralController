using aPC.Common;
using aPC.Common.Entities;
using aPC.Server.SceneHandlers;
using aPC.Server.Tests.Snapshots;
using System;

namespace aPC.Server.Tests.SceneHandlers
{
  class TestSceneHandler : SceneHandlerBase<TestSnapshot>
  {
    public TestSceneHandler(amBXScene xiScene, Action xiAction) : base (xiScene, xiAction)
    {
    }

    public override TestSnapshot GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      return new TestSnapshot(lFrame.Length);
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

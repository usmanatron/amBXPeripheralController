using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Tests.Snapshots;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  class TestSceneHandler : SceneHandlerBase<TestSnapshot>
  {
    public TestSceneHandler(amBXScene xiScene)
      : this(xiScene, null)
    {
    }

    public TestSceneHandler(amBXScene xiScene, Action xiAction) : base (xiScene, xiAction)
    {
    }

    public override TestSnapshot GetNextSnapshot(eDirection xiDirection)
    {
      return new TestSnapshot();
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
  }
}

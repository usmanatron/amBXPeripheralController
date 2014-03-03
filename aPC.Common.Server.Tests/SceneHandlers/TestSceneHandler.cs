using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Tests.Snapshots;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  class TestSceneHandler : SceneHandlerBase<TestSnapshot>
  {
    public TestSceneHandler() : this (null)
    {
    }

    public TestSceneHandler(Action xiAction) : base (xiAction)
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

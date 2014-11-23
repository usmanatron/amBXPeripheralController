using aPC.Common.Server.Conductors;
using aPC.Common.Server.Tests.Actors;
using aPC.Common.Server.Tests.SceneHandlers;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.Conductors
{
  internal class TestConductor : ConductorBase<TestSnapshot>
  {
    public TestConductor(eDirection direction, TestActor actor, TestSceneHandler handler)
      : base(direction, actor, handler)
    {
    }

    public override eComponentType ComponentType
    {
      get { throw new System.NotImplementedException(); }
    }

    // Don't need to log anything here.
    protected override void Log(string message)
    {
    }
  }
}
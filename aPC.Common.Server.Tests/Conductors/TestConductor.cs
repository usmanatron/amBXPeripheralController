using aPC.Common.Server.Conductors;
using aPC.Common.Server.Tests.Actors;
using aPC.Common.Server.Tests.SceneHandlers;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.Conductors
{
  internal class TestConductor : ConductorBase<TestSnapshot>
  {
    public TestConductor(eDirection xiDirection, TestActor xiActor, TestSceneHandler xiHandler)
      : base(xiDirection, xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType
    {
      get { throw new System.NotImplementedException(); }
    }

    // Don't need to log anything here.
    protected override void Log(string xiNotification)
    {
    }
  }
}
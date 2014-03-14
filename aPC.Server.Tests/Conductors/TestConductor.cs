using aPC.Common;
using aPC.Server.Conductors;
using aPC.Server.Tests.Snapshots;
using aPC.Server.Tests.Actors;
using aPC.Server.Tests.SceneHandlers;

namespace aPC.Server.Tests.Conductors
{
  class TestConductor : ConductorBase<TestSnapshot>
  {
    public TestConductor(eDirection xiDirection, TestActor xiActor, TestSceneHandler xiHandler) 
      : base (xiDirection, xiActor, xiHandler)
    {
    }
  }
}

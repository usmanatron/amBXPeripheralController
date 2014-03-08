using aPC.Common.Server.Conductors;
using aPC.Common.Server.Tests.Snapshots;
using aPC.Common.Server.Tests.EngineActors;
using aPC.Common.Server.Tests.SceneHandlers;

namespace aPC.Common.Server.Tests.Conductors
{
  class TestConductor : ConductorBase<TestSnapshot>
  {
    public TestConductor(eDirection xiDirection, TestEngineActor xiActor, TestSceneHandler xiHandler) 
      : base (xiDirection, xiActor, xiHandler)
    {
    }
  }
}

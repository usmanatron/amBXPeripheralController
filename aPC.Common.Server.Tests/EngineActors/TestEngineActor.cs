using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.EngineActors
{
  class TestEngineActor : EngineActorBase<TestSnapshot>
  {
    public TestEngineActor(IEngine xiEngine) : base(xiEngine)
    {
      TimesInvoked = 0;
    }

    public override void ActNextFrame(eDirection xiDirection, TestSnapshot xiSnapshot)
    {
      TimesInvoked++;
    }

    public int TimesInvoked;
  }
}

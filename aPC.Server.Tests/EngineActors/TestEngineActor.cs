using aPC.Common;
using aPC.Server.Engine;
using aPC.Server.Actors;
using aPC.Server.Tests.Snapshots;

namespace aPC.Server.Tests.EngineActors
{
  class TestEngineActor : ActorBase<TestSnapshot>
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

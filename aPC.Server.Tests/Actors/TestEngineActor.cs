using aPC.Common;
using aPC.Server.Engine;
using aPC.Server.Actors;
using aPC.Server.Tests.Snapshots;

namespace aPC.Server.Tests.Actors
{
  class TestActor : ActorBase<TestSnapshot>
  {
    public TestActor(IEngine xiEngine) : base(xiEngine)
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

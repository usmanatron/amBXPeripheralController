using aPC.Common;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.Actors
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

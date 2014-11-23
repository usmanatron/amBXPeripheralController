using aPC.Common.Server.Actors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.Actors
{
  internal class TestActor : ActorBase<TestSnapshot>
  {
    public TestActor(IEngine engine)
      : base(engine)
    {
      TimesInvoked = 0;
    }

    public override void ActNextFrame(eDirection direction, TestSnapshot snapshot)
    {
      TimesInvoked++;
    }

    public int TimesInvoked;
  }
}
using aPC.Common.Server.Actors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Tests.Snapshots;

namespace aPC.Common.Server.Tests.Actors
{
  internal class TestActor : IActor<TestSnapshot>
  {
    public TestActor(IEngine engine)
    {
      TimesInvoked = 0;
    }

    public void ActNextFrame(eDirection direction, TestSnapshot snapshot)
    {
      TimesInvoked++;
    }

    public int TimesInvoked;
  }
}
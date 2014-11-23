using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Tests.Snapshots
{
  internal class TestSnapshot : SnapshotBase
  {
    public TestSnapshot(int length)
      : base(10, length)
    {
    }
  }
}
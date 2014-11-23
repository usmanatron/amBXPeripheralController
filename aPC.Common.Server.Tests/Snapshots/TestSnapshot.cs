using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Tests.Snapshots
{
  internal class TestSnapshot : SnapshotBase
  {
    public TestSnapshot(int xiLength)
      : base(10, xiLength)
    {
    }
  }
}
using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;

namespace aPC.Common.Server.Tests.Snapshots
{
  class TestSnapshot : SnapshotBase
  {
    public TestSnapshot(int xiLength) : base(10, xiLength)
    {
    }
  }
}

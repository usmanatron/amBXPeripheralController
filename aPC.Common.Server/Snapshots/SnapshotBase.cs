namespace aPC.Common.Server.Snapshots
{
  public abstract class SnapshotBase
  {
    protected SnapshotBase(int length)
    {
      Length = length;
    }

    public int Length;
  }
}
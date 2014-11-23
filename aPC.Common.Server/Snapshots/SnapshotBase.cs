namespace aPC.Common.Server.Snapshots
{
  public abstract class SnapshotBase
  {
    protected SnapshotBase(int xiFadeTime, int xiLength)
    {
      FadeTime = xiFadeTime;
      Length = xiLength;
    }

    public int FadeTime;
    public int Length;
  }
}
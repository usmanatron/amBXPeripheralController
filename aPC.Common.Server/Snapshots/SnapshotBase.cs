namespace aPC.Common.Server.Snapshots
{
  public abstract class SnapshotBase
  {
    protected SnapshotBase(int fadeTime, int length)
    {
      FadeTime = fadeTime;
      Length = length;
    }

    public int FadeTime;
    public int Length;
  }
}
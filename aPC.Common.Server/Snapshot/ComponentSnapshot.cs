using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshot
{
  public class ComponentSnapshot : SnapshotBase
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int xiLength)
      : this(null, 0, xiLength)
    {
    }

    public ComponentSnapshot(Component xiComponent, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Component = xiComponent;
    }

    public bool IsComponentNull
    {
      get
      {
        return Component == null;
      }
    }

    public Component Component;
  }
}

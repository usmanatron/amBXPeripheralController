using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshots
{
  public class ComponentSnapshot<T> : SnapshotBase where T : Component
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int xiLength)
      : this(null, 10, xiLength)
    {
    }

    public ComponentSnapshot(T xiComponent, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Item = xiComponent;
    }

    public bool IsComponentNull
    {
      get
      {
        return Item == null;
      }
    }

    public T Item;
  }
}

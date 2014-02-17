using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  public class ComponentSnapshot : Snapshot
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int xiLength) : this(null, 0, xiLength)
    {
    }

    public ComponentSnapshot(Component xiItem, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Component = xiItem;
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

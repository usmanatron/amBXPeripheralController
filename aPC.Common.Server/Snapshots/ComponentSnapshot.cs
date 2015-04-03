using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshots
{
  public class ComponentSnapshot : SnapshotBase
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int length)
      : this(default(DirectionalComponent), length)
    {
    }

    public ComponentSnapshot(DirectionalComponent component, int length)
      : base(length)
    {
      Item = component;
    }

    public bool IsComponentNull
    {
      get
      {
        return Item == null;
      }
    }

    public DirectionalComponent Item;
  }
}
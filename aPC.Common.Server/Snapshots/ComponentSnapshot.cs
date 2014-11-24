using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshots
{
  public class ComponentSnapshot : SnapshotBase
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int length)
      : this(default(IComponent), 10, length)
    {
    }

    public ComponentSnapshot(IComponent component, int fadeTime, int length)
      : base(fadeTime, length)
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

    public IComponent Item;
  }
}
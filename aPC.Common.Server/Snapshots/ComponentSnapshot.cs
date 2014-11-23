using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshots
{
  public class ComponentSnapshot<T> : SnapshotBase where T : IComponent
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentSnapshot(int length)
      : this(default(T), 10, length)
    {
    }

    public ComponentSnapshot(T component, int fadeTime, int length)
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

    public T Item;
  }
}
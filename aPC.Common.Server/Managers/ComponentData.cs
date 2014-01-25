using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  public class ComponentData : Data
  {
    /// <summary>
    /// Used when a component is not available
    /// </summary>
    public ComponentData(int xiLength) : this(null, 0, xiLength)
    {
    }

    public ComponentData(Component xiItem, int xiFadeTime, int xiLength)
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

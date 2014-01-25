using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  public class ComponentData : Data
  {
    public ComponentData(Component xiItem, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Item = xiItem;
    }

    public bool IsComponentNull
    {
      get
      {
        return Item == null;
      }
    }

    public Component Item;
  }
}

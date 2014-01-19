using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  //qqUMI Need to think of a better name - used in ManagerBase
  public abstract class Data
  {
    public Data(int xiFadeTime, int xiLength)
    {
      FadeTime = xiFadeTime;
      Length = xiLength;
    }

    public int FadeTime;
    public int Length;
  }
}

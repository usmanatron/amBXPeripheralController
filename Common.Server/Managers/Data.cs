
namespace Common.Server.Managers
{
  //qqUMI Need to think of a better name - used in ManagerBase
  public class Data<T>
  {
    public Data(T xiItem, int xiFadeTime, int xiLength)
    {
      Item = xiItem;
      FadeTime = xiFadeTime;
      Length = xiLength;
    }

    public T Item;
    public int FadeTime;
    public int Length;
  }
}

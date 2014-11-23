using System.Threading;

namespace aPC.Common
{
  /// <summary>
  /// A wrapper to lock a struct.
  /// </summary>
  public class Locked<T> where T : struct
  {
    private T item;
    private readonly ReaderWriterLockSlim itemLocker;

    public Locked(T item)
    {
      itemLocker = new ReaderWriterLockSlim();
    }

    public T Get
    {
      get
      {
        lock (itemLocker)
        {
          return item;
        }
      }
    }

    public void Set(T newItem)
    {
      lock (itemLocker)
      {
        item = newItem;
      }
    }
  }
}
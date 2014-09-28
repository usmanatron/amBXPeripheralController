namespace aPC.Common.Server
{
  /// <summary>
  /// A bool with locking to ensure it's thread-safe.
  /// </summary>
  public class LockedBool
  {
    public bool Get
    {
      get
      {
        lock (mValueLocker)
        {
          return mValue;
        }
      }
    }

    public void SetTrue()
    {
      lock (mValueLocker)
      {
        mValue = true;
      }
    }

    public void SetFalse()
    {
      lock (mValueLocker)
      {
        mValue = false;
      }
    }

    private bool mValue;
    private readonly object mValueLocker = new object();
  }
}

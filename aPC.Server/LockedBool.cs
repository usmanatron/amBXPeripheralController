namespace aPC.Server
{
  /// <summary>
  /// A bool with locking to ensure it's thread-safe.
  /// </summary>
  public class LockedBool
  {
    public bool Value
    {
      get
      {
        lock (mValueLocker)
        {
          return mValue;
        }
      }
      set
      {
        lock (mValueLocker)
        {
          mValue = value;
        }
      }
    }

    private bool mValue;
    private readonly object mValueLocker = new object();
  }
}

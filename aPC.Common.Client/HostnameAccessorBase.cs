namespace aPC.Common.Client
{
  /// <summary>
  /// The default HostnameAccessor points to 
  /// localhost and does not support updating
  /// </summary>
  public class HostnameAccessor
  {
    public virtual string Get()
    {
      return "localhost";
    }

    public virtual void Update()
    {
    }
  }
}

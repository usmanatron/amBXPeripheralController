namespace aPC.Common.Client
{
  public abstract class HostnameAccessorBase
  {
    public abstract string Get();

    /// <remarks>
    /// By default, do nothing.
    /// </remarks>
    public virtual void Update()
    {
    }

    //public event OnHostnameChanged();
  }
}

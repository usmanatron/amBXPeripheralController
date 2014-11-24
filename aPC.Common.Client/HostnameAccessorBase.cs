using System.Collections.Generic;

namespace aPC.Common.Client
{
  /// <summary>
  /// The default HostnameAccessor points to
  /// localhost and does not support updating
  /// </summary>
  public class HostnameAccessor
  {
    private Locked<bool> hasChangedSinceLastCheck;
    private List<string> hostnames;

    public HostnameAccessor()
    {
      hostnames = new List<string> { "localhost" };
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public HostnameAccessor(List<string> hostnames)
    {
      this.hostnames = hostnames;
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public void Add(string hostname)
    {
      hostnames.Add(hostname);
      MarkUpdated();
    }

    public void ResetWith(List<string> hostnames)
    {
      this.hostnames = hostnames;
      MarkUpdated();
    }

    public IEnumerable<string> GetAll()
    {
      foreach (var hostname in hostnames)
      {
        yield return hostname;
      }
    }

    #region Watching for updates

    private void MarkUpdated()
    {
      hasChangedSinceLastCheck.Set(true);
    }

    public bool HasChangedSinceLastCheck()
    {
      if (hasChangedSinceLastCheck.Get)
      {
        hasChangedSinceLastCheck.Set(false);
        return true;
      }
      return false;
    }

    #endregion Watching for updates
  }
}
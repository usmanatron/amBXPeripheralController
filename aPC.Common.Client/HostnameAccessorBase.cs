using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Client
{
  /// <summary>
  /// The default HostnameAccessor points to
  /// localhost and does not support updating
  /// </summary>
  public class HostnameAccessor
  {
    private readonly Locked<bool> hasChangedSinceLastCheck;
    private List<string> hostnames;

    public HostnameAccessor()
    {
      hostnames = new List<string> { "localhost" };
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public HostnameAccessor(params string[] hostnames)
    {
      this.hostnames = hostnames.ToList();
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public void Add(string hostname)
    {
      hostnames.Add(hostname);
      MarkUpdated();
    }

    public void ResetWith(params string[] newHostnames)
    {
      this.hostnames = newHostnames.ToList();
      MarkUpdated();
    }

    public IEnumerable<string> GetAll()
    {
      return hostnames;
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
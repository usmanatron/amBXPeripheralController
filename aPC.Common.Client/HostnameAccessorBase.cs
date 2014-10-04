using aPC.Common;
using System.Collections.Generic;

namespace aPC.Common.Client
{
  /// <summary>
  /// The default HostnameAccessor points to 
  /// localhost and does not support updating
  /// </summary>
  public class HostnameAccessor
  {
    public HostnameAccessor()
    {
      hostnames = new List<string> { "localhost" };
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public HostnameAccessor(List<string> xiHostnames)
    {
      hostnames = xiHostnames;
      hasChangedSinceLastCheck = new Locked<bool>(false);
    }

    public void Add(string xiHostname)
    {
      hostnames.Add(xiHostname);
      MarkUpdated();
    }

    public void ResetWith(List<string> xiHostnames)
    {
      hostnames = xiHostnames;
      MarkUpdated();
    }

    public IEnumerable<string> GetAll()
    {
      foreach (var lHostname in hostnames)
      {
        yield return lHostname;
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

    #endregion

    private Locked<bool> hasChangedSinceLastCheck;
    private List<string> hostnames;
  }
}

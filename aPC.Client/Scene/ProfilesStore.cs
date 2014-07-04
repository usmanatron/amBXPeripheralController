using System;
using System.IO;

namespace aPC.Client.Scene
{
  static class ProfilesStore
  {
    public static string Directory = Path.Combine(Environment.CurrentDirectory, "Profiles");

    public static string GetFilenameWithoutExtension(string xiFullFilename)
    {
      var lFistCharAfterLastSlash = xiFullFilename.LastIndexOf(@"\", StringComparison.Ordinal) + 1;

      var lIndexOfLastPeriod = xiFullFilename.LastIndexOf(@".", StringComparison.Ordinal);
      var lXmlExtensionLength = xiFullFilename.Substring(lIndexOfLastPeriod).Length;

      var lFilenameLength = xiFullFilename.Length - lFistCharAfterLastSlash - lXmlExtensionLength;
      return xiFullFilename.Substring(lFistCharAfterLastSlash, lFilenameLength);
    }
  }
}

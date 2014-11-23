using System;
using System.IO;

namespace aPC.Client.Scene
{
  internal static class ProfilesStore
  {
    public static string Directory = Path.Combine(Environment.CurrentDirectory, "Profiles");

    public static string GetFilenameWithoutExtension(string fullFilename)
    {
      var firstCharAfterLastSlash = fullFilename.LastIndexOf(@"\", StringComparison.Ordinal) + 1;

      var indexOfLastPeriod = fullFilename.LastIndexOf(@".", StringComparison.Ordinal);
      var xmlExtensionLength = fullFilename.Substring(indexOfLastPeriod).Length;

      var filenameLength = fullFilename.Length - firstCharAfterLastSlash - xmlExtensionLength;
      return fullFilename.Substring(firstCharAfterLastSlash, filenameLength);
    }
  }
}